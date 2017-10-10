$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

$.FCA.page.createViewModel = (function (initialData) {
    var viewModel = {};
    debugger;

    viewModel.companyID = ko.observable(0);
    viewModel.jobList = ko.observableArray([]);

    viewModel.keywords = ko.observable(null);
    viewModel.selectedYear = ko.observable(null);



    var experienceYears = [];

    for (var i = 0; i < 11; i++) {
        experienceYears.push(i);
    }

    viewModel.yearsOfExperience = ko.observableArray(experienceYears);

   

    var onLoadJobsComplete = function (jsonData) {
        var mappedJobs = $.FCA.page.objectFactory.createJobs(jsonData);
        viewModel.jobList(mappedJobs)
    };

    var onLoadJobsFailure = function (jsonData) {

    };

    var onSaveEmployeeJobComplete = function (jsonData) {
        viewModel.searchJobs();
    };

    var onSaveEmployeeJobFailure = function (jsonData) {

    };

    viewModel.searchJobs = function () {
        var employeeID = initialData.EmployeeID;
        var filterArgs = { companyID: viewModel.companyID(), keyword: viewModel.keywords(), noOfYersOfExperience: viewModel.selectedYear(), employeeID: employeeID };
        $.FCA.page.dataAccess.loadJobs(filterArgs, onLoadJobsComplete, onLoadJobsFailure);
    }

    viewModel.searchJobsMatchingMyProfile = function () {
        viewModel.keywords(initialData.Keywords);
        viewModel.selectedYear(initialData.NoOfYersOfExperience);
        var filterArgs = { companyID: 0, keyword: initialData.Keywords, noOfYersOfExperience: initialData.NoOfYersOfExperience, employeeID: initialData.EmployeeID };
        $.FCA.page.dataAccess.loadJobs(filterArgs, onLoadJobsComplete, onLoadJobsFailure);
    }

    viewModel.saveEmployeeJob = function (row) {
        var jobID = row.id;
        var employeeID = initialData.EmployeeID;
        debugger;
        var data = { employeeID: employeeID, jobID: jobID, resume: '' };
        $.FCA.page.dataAccess.saveEmployeeJob(data, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure);
    }

    viewModel.clearSearch = function () {
        viewModel.keywords(null);
        viewModel.selectedYear(null);
        viewModel.searchJobs();
    }


    viewModel.viewJobDetails = function (row) {
        debugger;
        var jobID = row.id;
        window.location = '/Job/ViewJobDetailsView?jobID=' + jobID;
    }
   
    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    objectFactory.createJob = function (original) {
        return {
            id: original.ID,
            category: original.Category,
            companyID: original.CompanyID,
            jobDescription: original.JobDescription,
            keywords: original.Keywords,
            title: original.Title,
            yearsOfExperience: original.YearsOfExperience,
            companyName: original.CompanyName,
            alreadyApplied: original.AlreadyApplied
        }
    };

    objectFactory.createJobs = function (originals) {
        return ko.utils.arrayMap(originals, objectFactory.createJob);
    };

    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.loadJobs = function (filterArgs, onLoadJobsComplete, onLoadJobsFailure) {
        debugger;
        var params = { filterArgs: filterArgs };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.loadJobsLink, params, onLoadJobsComplete, onLoadJobsFailure);
    };

    dataAccess.saveEmployeeJob = function (data, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure) {
        debugger;
        var params = { employeeJob: data };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveEmployeeJobLink, params, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure);
    };

    return dataAccess;
})();