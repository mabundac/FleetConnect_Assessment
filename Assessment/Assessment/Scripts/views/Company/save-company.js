$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

//ko.validation.init({
//    errorElementClass: 'has-error',
//    errorMessageClass: 'help-block',
//    decorateElement: true,
//    insertMessages: true
//});
ko.validation.init({
    errorElementClass: "wrong-field",
    decorateElement: true,
    errorClass: 'wrong-field'
}, true);



$.FCA.page.createViewModel = (function (initialData) {
    var viewModel = {};
    var self = this;

    viewModel.validateNow = ko.observable(false);

    viewModel.isSuccessArlet = ko.observable(false);
    viewModel.isErrorArlet = ko.observable(false);
    viewModel.isWarningArlet = ko.observable(false);
    viewModel.feedbackMessage = ko.observable('Test message');

    //viewModel.userName = ko.observable(undefined).extend({ required: "Please enter a username terts" });
    //viewModel.password = ko.observable(undefined).extend({ required: "Please enter a password" });
    //viewModel.confirmPassword = ko.observable(undefined).extend({ required: "Please confirm a password" });;
    viewModel.name = ko.observable(undefined).extend({ required: "Please enter a company name" });;
    viewModel.address = ko.observable(undefined).extend({ required: "Please enter an address" });;
    viewModel.logo = ko.observable(undefined);
    viewModel.selectedCategory = ko.observable(undefined).extend({ required: "Please select a category" });;

    viewModel.logoReader = new FileReader();

    viewModel.categories = ko.observableArray(['Accounting', 'Agricultural Sciences', 'Construction', 'Information Technology']);

    var onSaveCompanyComplete = function (jsonData) {
        viewModel.isErrorArlet(false);
        viewModel.isSuccessArlet(true);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Company\'s profile saved!');
    };

    var onSaveCompanyFailure = function (jsonData) {

    };

    var uploadLogo = function () {
        var url = "/Company/PostLogo";
        var formData = new FormData();
        var file = document.getElementById("Logo").files[0];

        if (file) {
            formData.append(file.name, file);
            $.ajax({
                url: url,
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: formData,
                success: function (result) {
                    saveCompany();
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
    }

    viewModel.saveCompany = function () {
        debugger;
        viewModel.validateNow(true);
        viewModel.errors.showAllMessages(true);
        uploadLogo();
    }

    function base64ToBlob(base64, mime) {
        mime = mime || '';
        var sliceSize = 1024;
        var byteChars = window.atob(base64);
        var byteArrays = [];

        for (var offset = 0, len = byteChars.length; offset < len; offset += sliceSize) {
            var slice = byteChars.slice(offset, offset + sliceSize);

            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            var byteArray = new Uint8Array(byteNumbers);

            byteArrays.push(byteArray);
        }

        return new Blob(byteArrays, { type: mime });
    }

    var saveCompany = function () {
        debugger;
        var data = {
            ID: 0,
            //UserName: viewModel.userName(),
            //Password: viewModel.password(),
            Name: viewModel.name(),
            Address: viewModel.address(),
            Logo: viewModel.logo(),         
            Category: viewModel.selectedCategory()
        };

        var no = viewModel.errors().length;
        if (no <= 0) {
            $.FCA.page.dataAccess.saveCompany(data, onSaveCompanyComplete, onSaveCompanyFailure);
        }
    }


    viewModel.cancelSaveCompany = function () {

    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    //objectFactory.createJob = function (original) {
    //    return {
    //        id: original.ID,
    //        category: original.Category,
    //        companyID: original.CompanyID,
    //        jobDescription: original.JobDescription,
    //        keywords: original.Keywords,
    //        title: original.Title,
    //        yearsOfExperience: original.YearsOfExperience,
    //        companyName: original.CompanyName,
    //        alreadyApplied: original.AlreadyApplied
    //    }
    //};

    //objectFactory.createJobs = function (originals) {
    //    return ko.utils.arrayMap(originals, objectFactory.createJob);
    //};

    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.saveCompany = function (data, onSaveCompanyComplete, onSaveCompanyFailure) {
        var params = { company: data };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveCompanyLink, params, onSaveCompanyComplete, onSaveCompanyFailure);
    };

    return dataAccess;
})();

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


