var $main_color_yellow = '#f39c12';

// ========== document ready ==========
$(function() {

  // -------------- fastclick -------------- 
  FastClick.attach(document.body);

  // 縮圖及列表切換
  $('input[name="th"]:radio').change(function(){
    var $this = $(this);  
    var target = $this.parents('.box-header').siblings('.box-body');
    if($this.val() === '1'){
      target.addClass('table-large');
    }else{
      target.removeClass('table-large');
    }
  }); 

  //- 訂單-取消採購
  $('.btn-shopping-cancel').click(function(){
    swal({
      padding: 30,
      title: '是否真的要取消採購?',
      confirmButtonText: '確定不買',
      confirmButtonColor: $main_color_yellow,
      showCancelButton: true,
      cancelButtonText: '算了',
      reverseButtons: true,
      allowOutsideClick: false,
      input: 'textarea',
      inputPlaceholder: '取消原因(必填)',
      inputValidator: function (value) {
        return new Promise(function (resolve, reject) {
          if (value) {
            resolve()
          } else {
            reject('請輸入取消原因!')
          }
        })
      },
    }).then(function () {
      // 確定不買
      //- do somethine .....
      swal({
        title: 'accepted',
        confirmButtonColor: $main_color_yellow,
      })
    }, function (dismiss) {
      // dismiss can be 'cancel', 'overlay',
      // 'close', and 'timer'
      if (dismiss === 'cancel') {
        // 算了
        //- do somethine .....
        swal({
          title: 'canceled',
          confirmButtonColor: $main_color_yellow,
        })
      }
    }) 
  })

  //- 購物車-確認採購
  $('.btn-shopping-confirm').click(function(){
    swal({
      padding: 30,
      title: '是否確定採購?',
      text: '(直到我們的採購正式採購前，您都可以隨時請求取消採購)',
      confirmButtonText: '確定採購',
      confirmButtonColor: $main_color_yellow,
      showCancelButton: true,
      cancelButtonText: '取消',
      reverseButtons: true,
      allowOutsideClick: false,
    }).then(function () {
      //- do somethine .....

      // 確認採購
      fadeInMessage({
        title: '您的採購請求已經送出!',
        content: '(直到我們的採購正式採購前，您都可以隨時請求取消採購)',
        setTimeout: 3000,
      });
    }, function (dismiss) {
      // dismiss can be 'cancel', 'overlay',
      // 'close', and 'timer'
      if (dismiss === 'cancel') {
        // 取消採購
        //- do somethine .....
        
        swal({
          title: 'canceled',
          confirmButtonColor: $main_color_yellow,
        })
      }
    }) 
  })

});


// ---------- 向下出現 popup message --------
function fadeInMessage(object){
  var messageContainer = $('.message-popup');
  var timer = 3000;
  if(object){
    if(object.title){
      $('.message-title', messageContainer).html(object.title);
    }
    if(object.content){
      $('.message-content', messageContainer).html(object.content);
    }
    if(object.setTimeout){
      timer = object.setTimeout;
    }

    var timerId = null;
    var animatedDetect = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
    messageContainer.addClass('show animated fadeInDown').one(animatedDetect, 
      function(){
        setTimeout(function() {
          messageContainer.removeClass('fadeInDown').addClass('fadeOutUp').one(animatedDetect, 
            function(){
              messageContainer.removeClass('show animated fadeOutUp');
            }
          );
        }, timer);
      }
    );
  }

}
// ========== end document ready ==========





