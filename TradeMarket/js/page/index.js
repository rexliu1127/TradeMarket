if (typeof (Index) == 'undefined') {
    var Index = {};
}

(function (index) {
    index.loadProductTypeMenu = function () {
        var url = $.apiPath('getProductSuperType', '');
        $.ajaxAction(url, '', $('#tv_menu'), function (result) {
            var $menu = $('#tv_menu');
            try {
                $.each(result, function (i, pt) {
                    //$menu.append('<li><a href="#"><span class="btn-expand"></span><span>' + pt.ProductTypeName + '</span></a><ul class="treeview-menu" id="ul_' + pt.ProductTypeID + '"></ul></li>');
                    $menu.append('<li><a href="#"><span>' + pt.ProductTypeName + '</span></a><ul class="treeview-menu" id="ul_' + pt.ProductTypeID + '"></ul></li>');
                });
                console.log(result);

                url = $.apiPath('getProductType', '');
                $.ajaxAction(url, '', '', function (result) {
                    try {
                        var plusSpan = '<span class="btn-expand"></span>';
                        $.each(result, function (i, pt) {
                            var $ul = $('#ul_' + pt.SuperTypeID);
                            console.log($ul);
                            $ul.parent().find('a > span').before(plusSpan);
                            $ul.append('<li><a href="void:javascript(0)" onclick="Index.loadProduct(\'' + pt.ProductTypeName + '\',1, \'ProductCustomizeID\')" >' + pt.ProductTypeName + '</a></li>');
                            // $ul.append('<li><a href="/Mart/Index?productName=*&productTypeName=' + pt.ProductTypeName + '&showCount=5">' + pt.ProductTypeName + '</a></li>');
                        });
                        console.log(result);
                    }
                    catch (err) {
                        alert('Get product type occur error:' + err);
                    }
                }, '', 'GET');
            }
            catch (err) {
                alert('Get product type occur error:' + err);
            }
        }, '', 'GET');
    };
    //alert('load index.js');

    index.loadProduct = function (productTypeName, currentPage, sort) {
        //console.log('page:' + currentPage + ',sort:' + sort + ',pname=' + productName);
        var params = 'productName=*&productTypeName=' + productTypeName + '&currentPage=' + currentPage + '&showCount=5&sortColumn=' + sort;
        var url = $.apiPath('getProductPaging', params);
        $.ajaxAction(url, params, $('#divProduct'), function (result) {
            var obj = $('#pagination').twbsPagination({
                totalPages: result.Data.TotalPage,
                visiblePages: 5,
                initiateStartPageClick: false,
                prevText: '<span aria-hidden="true">&laquo;</span>',
                nextText: '<span aria-hidden="true">&raquo;</span>',
                onPageClick: function (event, page) {
                    console.log('onclick');
                    // loadData(page, sort);
                    Index.loadProduct(productTypeName, page, sort);
                }
            });
            console.info(obj.data());

            try {
                var $divProduct = $('#divProduct');
                $divProduct.empty();

                $.each(result.Data.ListOfProduct, function (i, pt) {
                    $divProduct.append(
                        '<tr class="tr">' +
                        '<td class="td check-td">' +
                        '<input type="checkbox">' +
                        '</td>' +
                        '<td class="td img-td">' +
                        '<div class="img-wrap"><img src="' + pt.ImageUrl + '" alt="' + pt.ProductName + '" title="' + pt.ProductName + '"></div>' +
                        '</td>' +
                        '<td class="td" data-title="代碼">' + pt.ProductCustomizeID + '</td>' +
                        '<td class="td" data-title="名稱">' + pt.ProductName + '</td>' +
                        '<td class="td qt-td" data-title="數量">' +
                        '<div class="qt-wrap">' +
                        '<input type="number" name="" placeholder="0">' +
                        '<div class="btn-group">' +
                        '<button class="btn" type="button"><i class="fa fa-minus-circle"></i></button>' +
                        '<button class="btn" type="button"><i class="fa fa-plus-circle"></i></button>' +
                        '</div>' +
                        '</div>' +
                        '</td>' +
                        '<td class="td" data-title="單位">' + pt.ProductUnitName + '</td>' +
                        '<td class="td behave-td">' +
                        '<button class="btn btn-default" type="button">加入購物車</button>' +
                        '<label>' +
                        '<input type="checkbox"><span class="box-checkbox"><i class="fa fa-archive"></i></span>' +
                        '</label>' +
                        '<label>' +
                        '<input type="checkbox"><span class="box-checkbox"><i class="fa fa-star"></i></span>' +
                        '</label>' +
                        '</td>' +
                        '</tr>'
                    );

                    console.log('append row ok');

                });
                console.log('res=' + result);


            }
            catch (err) {
                alert('Get product type occur error:' + err);
            }

        }, '', 'GET');

    };

    index.loadProductTypeMenu();
   // index.loadProduct('', 1, 'ProductCustomizeID');

})(Index);