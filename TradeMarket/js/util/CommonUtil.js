
var CommonUtil = {};

if (typeof AjaxUtil == 'undefined')
    throw('No AjaxUtil Object Exception, The CommonUtil can\'t execute normally.');

function Buffer(){
    var state = [];
    this.append = function(){
        for(var i= 0,n=arguments.length; i < n; i++){
            state.push(arguments[i]);
        };
        return this;
    };

    this.join =  function(separator){
        var sep = separator || '';
        var temp = state.join(sep);
        state = [];
        return temp;
    };

    this.clean = function(){
        state = [];
    };
}

function getParameterByName(name, url) {
    if (!url) {
        url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

(function(cu, au){

    cu.ajaxAction = au.ajaxAction;


   /* cu.buffer = {
        state : [],
        append : function(){
            for(var i= 0,n=arguments.length; i < n; i++){
                this.state.push(arguments[i]);
            };
        },
        join: function(separator){
            var sep = '' || separator;
          return this.state.join(sep);
        },
        clean: function(){
          this.state = [];
        },
    };*/

    cu.getWebContext = function () {
        //return '/' + window.location.pathname.split("/")[1] + '/';
        return au.webContext;
    };

    cu.getFunctionPath = function () {
        return window.location.pathname.split("/")[2];
    };

    cu.getCommonImagePath = function () {
        return CommonUtil.getWebContext() + 'assets/images/';
    };

    /**
     * 加入log功能，使用方法:log('訊息');
     */
    cu.log = function (d) {
        try {
            console.log(d);
        } catch (e) {

        }
    };

    /**
     * 複製物件
     */
    cu.clone = function (obj) {
        return JSON.parse(JSON.stringify(obj));
    };

    /**
     * 宣告 jQuery 判斷是否為空的共用函式
     *
     * 使用方式 : $.isBlank(變數) 判斷方式 : $.isBlank(" ") -> true $.isBlank("") -> true
     * $.isBlank("\n") -> true $.isBlank("a") -> false
     *
     * $.isBlank(null) -> true $.isBlank(undefined) -> true $.isBlank(false) -> true
     * $.isBlank([]) -> true
     *
     * @returns true: 是空的 , false: 不是空的
     */
    $.isBlank = function (obj) {
        return (!obj || $.trim(obj) === "");
    };

    /**
     * prototype area
     */
    //ie不支援startsWith與endsWith，加上以下code讓ie支援
    if (typeof String.prototype.startsWith != 'function') {
        String.prototype.startsWith = function (str) {
            return str.length > 0 && this.substring(0, str.length) === str;
        }
    }

    if (typeof String.prototype.endsWith != 'function') {
        String.prototype.endsWith = function (str) {
            return str.length > 0
                && this.substring(this.length - str.length, this.length) === str;
        }
    }


    Number.prototype.round = function (places) {
        places = Math.pow(10, places);
        return Math.round(this * places) / places;
    }

    /**
     * 把字串中的HTML標籤全部去除
     */
    String.prototype.stripHTML = function () {
        var matchTag = /<(?:.|\s)*?>|nbsp;/g;
        // Replace the tag
        return this.replace(matchTag, "");
    };

    /**
     * 將字串insert到指定的索引位置
     * @param index
     * @param string
     * @returns {*}
     */
    String.prototype.insert = function (index, string) {
        if (index > 0)
            return this.substring(0, index) + string + this.substring(index, this.length);
        else
            return string + this;
    };

    if (!Array.prototype.find) {
        Array.prototype.find = function (predicate) {
            if (this == null) {
                throw new TypeError('Array.prototype.find called on null or undefined');
            }
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }
            var list = Object(this);
            var length = list.length >>> 0;
            var thisArg = arguments[1];
            var value;

            for (var i = 0; i < length; i++) {
                value = list[i];
                if (predicate.call(thisArg, value, i, list)) {
                    return value;
                }
            }
            return undefined;
        };
    }
    //javascript indexOf for IE8
    function indexOfForIE8() {
        if (!Array.prototype.indexOf) {
            Array.prototype.indexOf = function (elt, from) {
                var len = this.length >>> 0;

                var from = Number(arguments[1]) || 0;
                from = (from < 0)
                    ? Math.ceil(from)
                    : Math.floor(from);
                if (from < 0)
                    from += len;

                for (; from < len; from++) {
                    if (from in this &&
                        this[from] === elt)
                        return from;
                }
                return -1;
            };
        }
    }

    //Object.keys() alternative for IE8 , must use this before Object.keys()
    function objectKeyForIE8() {
        if (!Object.keys) {
            Object.keys = (function () {
                'use strict';
                var hasOwnProperty = Object.prototype.hasOwnProperty,
                    hasDontEnumBug = !({toString: null}).propertyIsEnumerable('toString'),
                    dontEnums = [
                        'toString',
                        'toLocaleString',
                        'valueOf',
                        'hasOwnProperty',
                        'isPrototypeOf',
                        'propertyIsEnumerable',
                        'constructor'
                    ],
                    dontEnumsLength = dontEnums.length;

                return function (obj) {
                    if (typeof obj !== 'object' && (typeof obj !== 'function' || obj === null)) {
                        throw new TypeError('Object.keys called on non-object');
                    }

                    var result = [], prop, i;

                    for (prop in obj) {
                        if (hasOwnProperty.call(obj, prop)) {
                            result.push(prop);
                        }
                    }

                    if (hasDontEnumBug) {
                        for (i = 0; i < dontEnumsLength; i++) {
                            if (hasOwnProperty.call(obj, dontEnums[i])) {
                                result.push(dontEnums[i]);
                            }
                        }
                    }
                    return result;
                };
            }());
        }
    }
})(CommonUtil, AjaxUtil);

// CommonUtil convert to jQuery plugin
(function ($) {
    //$("div").log();
    $.fn.extend({
        log: function () {
            return this.each(function (i, v) {
                CommonUtil.log(this);
            });
        },
    });

    //$.log(msg);
    $.extend({
        log: function (m) {
            CommonUtil.log(m);
        },
        ajaxAction: function (handlerUrl, getQuery, $area_block, successHandler, dataType, methodType) {
            CommonUtil.ajaxAction (handlerUrl, getQuery, $area_block, successHandler, dataType, methodType);
        },

        commonImagePath: function (iconPath) {
            return CommonUtil.getCommonImagePath() + iconPath;
        },
        /**
         * 取得jsp位置的方法
         * @param page:jsp file name
         * @param params: queryString, 可為a=xx&b=yy 或者 {}的形式傳入
         * @param folderPath: jsp所在folder(optional)
         * @returns {string}
         */
        webHref: function (page, params, folderPath) {
            var queryStr = '';
            if (params) {
                if (typeof params == 'object') {
                    queryStr = $.param(params);
                } else
                    queryStr = params;

                queryStr = '?' + queryStr;
            }

            if (folderPath)
                return CommonUtil.getWebContext() + folderPath + '/' + page + queryStr;
            else
                return CommonUtil.getWebContext() + page + queryStr;
        },

        notNull: function (val) {
            if (!val)
                return '';
            else
                return val;
        },
    });
})(jQuery);
