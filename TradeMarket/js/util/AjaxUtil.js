
var AjaxUtil = {
    webContext: function () {
        var webContext = '/' + window.location.pathname.split("/")[1] + '/';
        if (webContext.indexOf('TradeMarket') < 0)
            webContext = '/';

        return webContext;
    }

    

};
(function(au){
    $.ajaxSetup({
        // cache:false,//避免ajax在發生錯誤時就不做ajax了
        type: 'POST',
        dataType: 'json',
        timeout: (20 * 60 * 1000), //20分鐘
        error: function (jqXHR, textStatus, errorThrown) {
            switch (textStatus) {
                case 'abort':
                    // do nothing
                    break;
                default:
                    CommonUtil.log('資料庫或網路連線發生異常，請洽詢管理員協助！<br>jqXHR:' + jqXHR + 'textStatus:' + textStatus + "<br>errorThrown:" + errorThrown);
            }
        },
        statusCode: {
            401: function(){
                window.location = au.webContext + 'admin/login.jsp';
            },
            901: function () { // 考慮是否要用此種方式呈現 Session
                // Timeout
                //window.location = 'SessionTimeout.jsp';
            },
            404: function () {
                CommonUtil.log('url找不到！');
            },

        }
    });

    /**
     * 顯示BlockUI
     */
    var showBlockUI = function (msg, $area) {
        if ($.isBlank(msg))
            msg = '載入中...';
        if ($.blockUI && $.unblockUI) {
            var blockui_option = {
                css: {
					fontSize: '15px',
	                borderWidth:'1px',
	                borderColor: '#0b314a',
                    padding: '5px',
                    backgroundColor: '#0072be',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: 1,
                    color: '#fff',
                    'font-family': 'Lucida Grande,Lucida Sans,Arial,sans-serif',
                    'z-index':2000,
                },
                message: '<table align="center" border="0"><tr><td><img style="vertical-align:middle;width:16px;height:16px" src="' + au.webContext() + 'images/ajax-loader.gif" /></td><td style="white-space: nowrap"><h2>　' + msg + '</h2></td></tr></table>',

                overlayCSS: {
                    backgroundColor: '#000',
                    opacity: 0.5,
                    cursor: 'wait'
                }
            };
            if ($.isBlank($area)){
            	//alert(blockui_option.message);
                $.blockUI(blockui_option);	
            }
            else
                $area.block(blockui_option);
        }
    };

    au.getBlockUIFunction = function(){
        return showBlockUI;
    }

    /**
     * 處理Ajax回來後的共同訊息，回傳boolean值(isContinue)，true表示要繼續執行，false表示要中斷執行。
     */
    var processAjaxMsg = function (data) {
        var isContinue = true;
        if (data['successMsg'] != null) {//顯示處理成功的訊息
            CommonUtil.log(data['successMsg']);
        }
        if (data['infoMsg'] != null) {//顯示一般提示訊息
            CommonUtil.log(data['infoMsg']);
        }
        if (data['warnMsg'] != null) {//顯示警告訊息
            CommonUtil.log(data['warnMsg']);
        }
        if (data['errorMsg'] != null) {//顯示錯誤訊息,像是一般的新增,修改,刪除失敗的訊息。
            CommonUtil.log(data['errorMsg']);
        }
        if (data['debugMsg'] != null) {//顯示後端程式要送到前端console顯示的訊息
            CommonUtil.log(data['debugMsg']);
        }
        if (data['exceptionMsg'] != null) {//顯示後端程式發生例外時的訊息
            CommonUtil.log(data['exceptionMsg']);//上線暫時拿掉
            isContinue = false;
        }
        return isContinue;
    };


    /**
     * 執行ajax取得資料
     * @Param handler String, the url to get data
     * @Param action String, the action for server controller to distinct action
     * @Param getQuery Method Pointer, get query conditions
     * @Param $area_block null:不執行blockUI; '':整個畫面blockUI; !='':區塊性blockUI
     * @Param successHadnler Method Pointer, callback method to handle data which get from server
     * ＠Param dataType::執行ajax後的回傳結果，不給值預設為JSON
     * ＠Param methodType: request 是以GET or  POST發出，不給值預設為POST.
     */
    au.ajaxAction = function (handlerUrl, getQuery, $area_block, successHandler, dataType, methodType) {
        var obj = getQuery;
        //obj.ajaxAction = action;
        var dataType = dataType || 'JSON';
        var methodType = methodType || 'POST';
        if ($area_block != null)
            showBlockUI('', $area_block);
        $.ajax({
            url: handlerUrl,
            type: methodType,
            dataType: dataType,
            data: obj,
            success: function (data) {
                try {
                    successHandler(data);
                    processAjaxMsg(data);

                    if ($area_block != null) {
                        ($area_block ? $area_block.unblock() : $.unblockUI() );
                    }
                }
                catch (err) {
                    if ($area_block != null) {
                        ($area_block ? $area_block.unblock() : $.unblockUI() );
                    }
                    if (window.console) {
                        console.log('error message:' + err.message);
                        console.log('stack:' + err.stack);
                    }
                }
            },
            complete: function () {
                if ($area_block != null) {
                    ($area_block ? $area_block.unblock() : $.unblockUI() );
                }
            }
        });
    };
    au.showBlockUI = showBlockUI;
})(AjaxUtil);
