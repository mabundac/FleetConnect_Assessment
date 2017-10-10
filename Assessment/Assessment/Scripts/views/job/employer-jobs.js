$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

$.FCA.page.createViewModel = (function (initialData) {
    var viewModel = {};

    viewModel.companyID = ko.observable(0);
    viewModel.jobListItems = ko.observableArray([]);

    viewModel.keywords = ko.observable(null);
    viewModel.selectedYear = ko.observable(null);

    var experienceYears = [];

    for (var i = 0; i < 11; i++) {
        experienceYears.push(i);
    }

    viewModel.yearsOfExperience = ko.observableArray(experienceYears);

    var onLoadJobListComplete = function (jsonData) {
        debugger;
        var data = jsonData.Data;
        var mappedJobs = $.FCA.page.objectFactory.createJobListItems(data);
        viewModel.jobListItems(mappedJobs)
       // viewModel.jobList(jsonData)
    };

    var onLoadJobListFailure = function (jsonData) {

    };

    viewModel.searchJobs = function () {
        var filterArgs = { companyID: viewModel.companyID(), keyword: viewModel.keywords(), noOfYersOfExperience: viewModel.selectedYear(), employeeID: 0 };
        $.FCA.page.dataAccess.loadJobList(filterArgs, onLoadJobListComplete, onLoadJobListFailure);
    }

    viewModel.clearSearch = function () {
        viewModel.keywords(null);
        viewModel.selectedYear(null);
        viewModel.searchJobs();
    }

    viewModel.editJob = function (row) {
        var jobID = row.jobID;
        location.href = '/Job/SaveJobView?jobID=' + jobID;
    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    objectFactory.createJobListItem = function (original) {
        return {
            jobID: original.JobID,
            jobTitle: original.JobTitle,
            yearsOfExperience: original.YearsOfExperience,
            jobDescription: original.JobDescription,
            keywords: original.Keywords,
            category: original.Category,
            noOfApplications: 0,
            noOfApplications: original.NoOfApplications != null ? original.NoOfApplications : 0,
            companyID: original.CompanyID,
            employeeJobID: original.EmployeeJobID,
            employeeID: original.EmployeeID,
            resume: original.Resume,
            dateLogged: original.DateLogged != null ? $.FCA.objectFactory.formatJsonDate(original.DateLogged) : null,
            url: "/Job/EmployerAppliedJobsView?jobID=" + original.JobID
        }
    };

    objectFactory.createJobListItems = function (originals) {
        return ko.utils.arrayMap(originals, objectFactory.createJobListItem);
    };

    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.loadJobList = function (filterArgs, onLoadJobListComplete, onLoadJobListFailure) {
        debugger;
        var params = { filterArgs: filterArgs };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.loadJobListLink, params, onLoadJobListComplete, onLoadJobListFailure);
    };

    //dataAccess.saveEmployeeJob = function (data, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure) {
    //    debugger;
    //    var params = { employeeJob: data };
    //    $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveEmployeeJobLink, params, onSaveEmployeeJobComplete, onSaveEmployeeJobFailure);
    //};

    return dataAccess;
})();