var User = {};
var Constants = {
    isLogin: 0,
};

//the CommonUtil obj probably defined in CommonUtil.js
if (typeof(CommonUtil) == 'undefined'){
    $.log('CommonUtil is undefined, instantiate one.');

}

(function(cu){
    cu.apiUrl = 'http://220.130.10.50:6868/api/WebApi/';
    cu.msg = {
        addSuccess:  '新增成功',
        addFailed:   '新增失敗',
        editSuccess: '修改成功',
        editFailed:  '修改失敗',
        deleteSuccess: '删除成功',
        deleteFailed: '刪除失敗'
    };

    //在此專案無用
    cu.getHandlerPath = function(functionPath, action, params) {
        var queryStr = '';
        if (params) {
            if (typeof params == 'object') {
                queryStr = $.param(params);
            } else
                queryStr = params;

            queryStr = '?' + queryStr;
        }

        return CommonUtil.getWebContext() + 'admin/' + functionPath + '/' + action + '.json' + queryStr;
    };

    //取得api Url，若有參數可以物件形式{'a':1, 'b':2}傳入或是queryString形式(key1=value1&key2=value2)
    cu.getApiPath = function(action, params) {
        var queryStr = '';
        if (params) {
            if (typeof params == 'object') {
                queryStr = $.param(params);
            } else
                queryStr = params;

            queryStr = '?' + queryStr;
        }

        return CommonUtil.apiUrl + action  + queryStr;
    };

    cu.getCloudPublicId = function(imgUrl){
        var publicId =  '';
        if (imgUrl){
            var startIndex = imgUrl.lastIndexOf('/')+1;
            var endIndex = imgUrl.lastIndexOf('.');
            publicId = imgUrl.substring(startIndex, endIndex);
        }
        return publicId
    }

    //CommonUtil convert to jQuery plugin
    jQuery.extend({
        handlerPath: function(functionPath, action){
            return CommonUtil.getHandlerPath(functionPath, action);
        },

        apiPath: function(action, params){
            return CommonUtil.getApiPath(action, params);
        },

      
    });
})(CommonUtil);


// CommonUtil convert to jQuery plugin
/*(function ($) {

    //$.log(msg);
    $.extend({
        handlerPath: function(functionPath, action){
            return CommonUtil.getHandlerPath(functionPath, action);
        },

        fullGoodsPath: function (goodsId, iconPath) {
            return CommonUtil.getFullGoodsPath(goodsId, iconPath);

        },

        fullClientPath: function (clientId, iconPath) {
            return CommonUtil.getFullClientPath(clientId, iconPath);
        },
        fullSupplierPath: function (supplierId, iconPath) {
            return CommonUtil.getFullSupplierPath(supplierId, iconPath);
        },

    });
})(jQuery);*/

