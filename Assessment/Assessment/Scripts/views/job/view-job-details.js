$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

$.FCA.page.createViewModel = (function (initialData) {
    var viewModel = {};
    var self = this;
    debugger;
    viewModel.validateNow = ko.observable(false);

    viewModel.isSuccessArlet = ko.observable(false);
    viewModel.isErrorArlet = ko.observable(false);
    viewModel.isWarningArlet = ko.observable(false);
    viewModel.feedbackMessage = ko.observable('Test message');

    var jobTitle = initialData != null ? initialData.Title : undefined;
    var jobDescription = initialData != null ? initialData.JobDescription : undefined;
    var keywords = initialData != null ? initialData.Keywords : undefined;
    var selectedCategory = initialData != null ? initialData.Category : undefined;
    var yearsOfExperience = initialData != null ? initialData.YearsOfExperience : undefined;
    var jobID = initialData != null ? initialData.ID : 0;
    var companyID = initialData != null ? initialData.CompanyID : 0;

    viewModel.jobTitle = ko.observable(jobTitle);
    viewModel.jobDescription = ko.observable(jobDescription);
    viewModel.keywords = ko.observable(keywords);
    viewModel.category = ko.observable(selectedCategory);
    viewModel.yearsOfExperience = ko.observable(yearsOfExperience);
    viewModel.jobID = ko.observable(jobID);
    viewModel.companyID = ko.observable(companyID);
    
    var onSaveEmployeeJobComplete = function (jsonData) {
        viewModel.isErrorArlet(false);
        viewModel.isSuccessArlet(true);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Job application sent successfully!');
    };

    var onSaveEmployeeJobFailure = function (jsonData) {
        viewModel.isErrorArlet(true);
        viewModel.isSuccessArlet(false);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Error when sending job application!');
    };

    viewModel.saveEmployeeJob = function () {
        var employeeID = initialData.EmployeeID;
        debugger;
        var data = { employeeID: employeeID, jobID: viewModel.jobID(), resume: '' };
        $.FCA.page.dataAccess.saveEmployeeJob(data, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure);
    }
    
    viewModel.cancelSaveEmployeeJob = function () {
        location.href = '/Job/Index';
    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.saveEmployeeJob = function (data, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure) {
        var params = { employeeJob: data };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveEmployeeJobLink, params, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure);
    };

    return dataAccess;
})();
