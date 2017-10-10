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
    var selectedYear = initialData != null ? initialData.YearsOfExperience : undefined;
    var jobID = initialData != null ? initialData.ID : 0;
    var companyID = initialData != null ? initialData.CompanyID : 0;

    viewModel.jobTitle = ko.observable(jobTitle).extend({ required: "Please enter a job title" });
    viewModel.jobDescription = ko.observable(jobDescription).extend({ required: "Please enter a job description" });
    viewModel.keywords = ko.observable(keywords).extend({ required: "Please enter keywords" });
    viewModel.selectedCategory = ko.observable(selectedCategory).extend({ required: "Please select a category" });
    viewModel.selectedYear = ko.observable(selectedYear).extend({ required: "Please select years of experience" });
    viewModel.jobID = ko.observable(jobID);
    viewModel.companyID = ko.observable(companyID);

    var experienceYears = [];

    for (var i = 0; i < 11; i++) {
        experienceYears.push(i);
    }

    viewModel.yearsOfExperience = ko.observableArray(experienceYears);
    viewModel.categories = ko.observableArray(['Accounting', 'Agricultural Sciences', 'Construction', 'Information Technology']);

    var onSaveJobComplete = function (jsonData) {
        viewModel.isErrorArlet(false);
        viewModel.isSuccessArlet(true);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Job\'s details saved!');
    };

    var onSaveJobFailure = function (jsonData) {

    };

    viewModel.saveJob = function () {
        viewModel.validateNow(true);
        viewModel.errors.showAllMessages(true);
        
        var data = {
            ID: viewModel.jobID(),
            Title: viewModel.jobTitle(),
            yearsOfExperience: viewModel.selectedYear(),
            JobDescription: viewModel.jobDescription(),
            Keywords: viewModel.keywords(),
            CompanyID: viewModel.companyID(),
            Category: viewModel.selectedCategory()
        };

        var no = viewModel.errors().length;
        if (no <= 0) {
            $.FCA.page.dataAccess.saveJob(data, onSaveJobComplete, onSaveJobFailure);
        }

        if (initialData != null) {
            debugger;
        }
    }


    viewModel.cancelSaveJob = function () {
        location.href = '/Job/EmployerJobsView';
    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    //objectFactory.createJob = function (original) {
    //    return {
    //        id: original.ID,
    //        category: original.Category,
    //        companyID: original.JobID,
    //        jobDescription: original.JobDescription,
    //        keywords: original.Keywords,
    //        title: original.Title,
    //        yearsOfExperience: original.YearsOfExperience,
    //        companyName: original.JobName,
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

    dataAccess.saveJob = function (data, onSaveJobComplete, onSaveJobFailure) {
        var params = { job: data };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveJobLink, params, onSaveJobComplete, onSaveJobFailure);
    };

    return dataAccess;
})();
