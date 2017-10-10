$.FCA = function () {
    var $this = this;
    var fca = {};

    fca.ajaxTools = {};

    return fca;
}();

$.FCA.objectFactory = (function () {
    var objectFactory = {};


    objectFactory.formatDateTime = function (jsonDate) {
        var date = new Date(parseInt(jsonDate.substr(6)));

        var year = date.getFullYear();
        var month = date.getMonth();
        var day = date.getDate();
        var hour = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        var milliSeconds = date.getMilliseconds();

        month += 1;

        if (parseInt(day) < 10) {
            day = '0' + day;
        }

        if (parseInt(month) < 10) {
            month = '0' + month;
        }

        var strDate = year + '-' + month + '-' + day + ' ' + hour + ':' + minutes + ':' + seconds + '.' + milliSeconds;

        return strDate;
    };

    objectFactory.formatJsonDate = function (jsonDate) {
        var dateObj = new Date(parseInt(jsonDate.substr(6)));
        var day = dateObj.getDate();
        var month = dateObj.getMonth() + 1;
        if (parseInt(day) < 10) {
            day = '0' + day;
        }
        if (parseInt(month) < 10) {
            month = '0' + month;
        }


        var formattedDate = (day + "/" + month + "/" + dateObj.getFullYear());
        return formattedDate;
    };

    objectFactory.formatServerDate = function (jsonDate) {
        var dateObj = new Date(parseInt(jsonDate.substr(6)));
        var strDay = dateObj.getDate();
        var strMonth = dateObj.getMonth() + 1;
        var strYear = dateObj.getFullYear();

        if (parseInt(strDay) < 10) {
            strDay = '0' + strDay;
        }

        if (parseInt(strMonth) < 10) {
            strMonth = '0' + strMonth;
        }

        var formattedDate = (strYear + "-" + strMonth + "-" + strDay);
        return formattedDate;
    };

    objectFactory.formatUserDefinedDate = function (strDate) {
        var strDay = strDate.substr(0, strDate.indexOf('/'));
        var strMonth = strDate.substr(strDay.length + 1, strDate.indexOf('/'));
        var strYear = strDate.substr((strDate.length - 4), 4);
        var formattedDate = null;

        if (isNaN(strDay) && isNaN(strMonth) && isNaN(strYear)) {
            return null;
        } else {
            formattedDate = (strYear + "-" + strMonth + "-" + strDay);
        }

        return formattedDate;
    };

    return objectFactory;
})();

$.FCA.ajaxTools = (function () {
    var $this = this;
    var ajaxTools = {};

    ajaxTools.submitAjaxGetRequest = function (targetUrl, successFunction) {
        $.get(targetUrl, successFunction);
    };

    ajaxTools.submitAjaxPostJsonRequest = function (targetUrl, jsonData, successFunction, errorFunction) {

        var jsonString = JSON.stringify(jsonData);

        $.ajax({
            type: 'POST',
            url: targetUrl,
            data: jsonString,
            success: successFunction,
            error: errorFunction,
            contentType: "application/json",
            dataType: 'json'
        });
    };

    var reloadScrollBars = function () {
        document.documentElement.style.overflow = 'auto';
        document.body.scroll = "yes";
    };

    var unloadScrollBars = function () {
        document.documentElement.style.overflow = 'hidden';
        document.body.scroll = "no";
    };

    ajaxTools.startAnimation = function () {
        $('html,body').scrollTop(0);
        // unloadScrollBars();
        var cvr = document.getElementById("animationCover");
        cvr.style.display = "block";
        cvr.style.width = screen.width;
        cvr.style.height = screen.height;
    };

    ajaxTools.stopAnimation = function () {
        var cvr = document.getElementById("animationCover");
        cvr.style.display = "none";
        // reloadScrollBars();
    };

    return ajaxTools;
})();


$.FCA.modalTools = function () {
    var modalTools = {};

    modalTools.startPageCover = function () {

        var cvr = document.getElementById("animationCover");
        cvr.style.display = "block";
        if (document.body.style.overflow = "hidden") {
            cvr.style.width = screen.width;
            cvr.style.height = screen.height;
        }
    };

    modalTools.closePageCover = function () {
        var cvr = document.getElementById("animationCover");
        cvr.style.display = "none";
    };

    modalTools.showConfirmModalDialog = function (yesFunction) {
        $("#dialog-confirm").css("visibility", "visible");
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 140,
            modal: true,
            buttons: [{
                text: "Close",
                click: yesFunction,
                class: "button"
            }, {
                text: "Cancel",
                click: function () {
                    $(this).dialog("close");
                    ajaxTools.closePageCover();
                },
                class: "button"
            }]
        });
    };

    var domModal = null;

    modalTools.close = function () {
        if (domModal != null) {
            $(domModal).dialog("close");
        }
    };

    modalTools.createModalDialog = function (dialogId, modalHeight, modalWidth) {
        domModal = $("#" + dialogId);
        $(domModal).css("visibility", "visible");
        $(domModal).dialog({
            modal: true,
            resizable: false,
            position: ['center', 'center'],
            show: 'blind',
            hide: 'blind',
            width: modalWidth,
            height: modalHeight
        });

        // $(domModal).appendTo("body").constructor("show");
    };

    return modalTools;
}();


ko.extenders.required = function (target, overrideMessage) {
    //add some sub-observables to our observable
    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    //define a function to do validation
    function validate(newValue) {
        target.hasError(newValue ? false : true);
        target.validationMessage(newValue ? "" : overrideMessage || "This field is required");
    }

    //initial validation
    //validate(target());

    //validate whenever the value changes
    target.subscribe(validate);

    //return the original observable
    return target;
};









