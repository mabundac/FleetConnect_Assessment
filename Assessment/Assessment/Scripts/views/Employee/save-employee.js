$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

ko.validation.init({
    errorElementClass: "wrong-field",
    decorateElement: true,
    errorClass: 'wrong-field'
}, true);


$.FCA.page.createViewModel = (function (initialData) {
    debugger;
    var viewModel = {};
    var self = this;

    viewModel.validateNow = ko.observable(false);

    viewModel.isSuccessArlet = ko.observable(false);
    viewModel.isErrorArlet = ko.observable(false);
    viewModel.isWarningArlet = ko.observable(false);
    viewModel.feedbackMessage = ko.observable('Test message');
    var firstName = initialData.FirstName;
    var laststName = initialData.LastName;

    viewModel.firstName = ko.observable(firstName).extend({ required: "Please enter a first name" });
    viewModel.surname = ko.observable(laststName).extend({ required: "Please enter a surname" });
    viewModel.address = ko.observable(undefined).extend({ required: "Please enter an address" });
    viewModel.resume = ko.observable(undefined);
    viewModel.keywords = ko.observable(undefined).extend({ required: "Please enter keywords" });
    viewModel.selectedCategory = ko.observable(undefined).extend({ required: "Please select a category" });
    viewModel.selectedYear = ko.observable(undefined).extend({ required: "Please select years of experience" });

    viewModel.resumeReader = new FileReader();

    viewModel.categories = ko.observableArray(['Accounting', 'Agricultural Sciences', 'Construction', 'Information Technology']);
    var experienceYears = [];

    for (var i = 0; i < 11; i++) {
        experienceYears.push(i);
    }

    viewModel.yearsOfExperience = ko.observableArray(experienceYears);

    
    var onSaveJobSeekerComplete = function (jsonData) {
        viewModel.isErrorArlet(false);
        viewModel.isSuccessArlet(true);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Job seeker profile saved!');
        window.location = '/Job/Index';
    };

    var onSaveJobSeekerFailure = function (jsonData) {
        viewModel.isErrorArlet(true);
        viewModel.isSuccessArlet(false);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Error when saving details!');
    };

    var uploadResume = function () {
        var url = "/Employee/PostResume";
        var formData = new FormData();
        var file = document.getElementById("Resume").files[0];

        if (file) {
            formData.append(file.name, file);
            $.ajax({
                url: url,
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: formData,
                success: function (result) {
                    saveJobSeeker();
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
    }

    viewModel.saveJobSeeker = function () {
        debugger;
        viewModel.validateNow(true);
        viewModel.errors.showAllMessages(true);
            uploadResume();
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

    var saveJobSeeker = function () {
        debugger;
        var data = {
            ID: 0,          
            Name: viewModel.surname() + ", " + viewModel.firstName(),
            Address: viewModel.address(),
            Resume: viewModel.resume(),
            Keywords: viewModel.keywords(),
            Category: viewModel.selectedCategory(),
            YearsOfExperience: viewModel.selectedYear()
        };

        var no = viewModel.errors().length;
        if (no <= 0) {
            $.FCA.page.dataAccess.saveJobSeeker(data, onSaveJobSeekerComplete, onSaveJobSeekerFailure);
        }           
    }  
 
    viewModel.cancelSaveJobSeeker = function () {
      
    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};


    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.saveJobSeeker = function (data, onSaveJobSeekerComplete, onSaveJobSeekerFailure) {
        var params = { employee: data };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveJobSeekerLink, params, onSaveJobSeekerComplete, onSaveJobSeekerFailure);
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


