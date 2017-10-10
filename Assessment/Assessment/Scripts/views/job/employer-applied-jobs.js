$.FCA.page = (function () {
    var page = {};

    page.objectFactory = {};
    page.dataAccess = {};
    page.links = {};

    return page;
})();

$.FCA.page.createViewModel = (function (initialData) {
    var viewModel = {};
    
    viewModel.isSuccessArlet = ko.observable(false);
    viewModel.isErrorArlet = ko.observable(false);
    viewModel.isWarningArlet = ko.observable(false);
    viewModel.feedbackMessage = ko.observable('');

    var pageHeader = "Jobs Applied For: " + initialData.Title;
    var jobID = initialData.ID;

    viewModel.employerAppliedjobs = ko.observableArray([]);
    viewModel.pageHeader = ko.observable(pageHeader);
    
    var onLoadEmployerAppliedjobsComplete = function (jsonData) {
        debugger;
        var data = jsonData.Data;
        var mappedJobs = $.FCA.page.objectFactory.createEmployerAppliedjobs(data);
        viewModel.employerAppliedjobs(mappedJobs)
    };

    var onLoadEmployerAppliedjobsFailure = function (jsonData) {
        viewModel.isErrorArlet(true);
        viewModel.isSuccessArlet(false);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Error when loading applied jobs!');
    };

    viewModel.loadEmployerAppliedJobs = function (row) {
        debugger;
        var job_id = initialData.ID;
        $.FCA.page.dataAccess.loadEmployerAppliedJobs(job_id, onLoadEmployerAppliedjobsComplete, onLoadEmployerAppliedjobsFailure);
    }

    var onSaveJobApplicationStatusComplete = function () {
        viewModel.isErrorArlet(false);
        viewModel.isSuccessArlet(true);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Job status saved!');
        viewModel.loadEmployerAppliedJobs();
    };

    var onSaveJobApplicationStatusFailure = function () {
        viewModel.isErrorArlet(true);
        viewModel.isSuccessArlet(false);
        viewModel.isWarningArlet(false);
        viewModel.feedbackMessage('Error when saving job status');
    };
    
    viewModel.acceptJob = function (row) {
        var data = {
            employeeJobID: row.employeeJobID,
            status: "Accepted"
        }

        $.FCA.page.dataAccess.saveJobApplicationStatus(data, onSaveJobApplicationStatusComplete, onSaveJobApplicationStatusFailure);
    }

    viewModel.rejectJob = function (row) {
        var data = {
            employeeJobID: row.employeeJobID,
            status: "Rejected"
        }

        $.FCA.page.dataAccess.saveJobApplicationStatus(data, onSaveJobApplicationStatusComplete, onSaveJobApplicationStatusFailure);;
    }

    viewModel.backToJobList = function () {
        location.href = '/Job/EmployerJobsView';
    }

    return viewModel;
});

$.FCA.page.objectFactory = (function () {
    var objectFactory = {};

    objectFactory.createEmployerAppliedjob = function (original) {
        return {
            jobID: original.JobID,
            jobTitle: original.JobTitle,
            yearsOfExperience: original.YearsOfExperience,
            employeeName: original.Name,
            jobDescription: original.JobDescription,          
            employeeID: original.EmployeeID,
            employeeJobID: original.EmployeeJobID,
            resume: original.Resume,
            dateLogged: $.FCA.objectFactory.formatJsonDate(original.DateApplied),
            status: original.Status,
            canBeAccepted: original.Status != "Accepted" ? true : false,
            canBeRejected: original.Status != "Rejected" ? true : false,
            downloadUrl: '/Job/DownloadResume?employeeJobID=' + original.EmployeeJobID
        }
    };

    objectFactory.createEmployerAppliedjobs = function (originals) {
        return ko.utils.arrayMap(originals, objectFactory.createEmployerAppliedjob);
    };

    return objectFactory;
})();

$.FCA.page.dataAccess = (function () {
    var dataAccess = {};

    dataAccess.loadEmployerAppliedJobs = function (jobID, onLoadEmployerAppliedjobsComplete, onLoadEmployerAppliedjobsFailure) {
        debugger;
        var params = { jobID: jobID };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.loadJobListLink, params, onLoadEmployerAppliedjobsComplete, onLoadEmployerAppliedjobsFailure);
    };

    dataAccess.saveJobApplicationStatus = function (data, onSaveJobApplicationStatusComplete, onSaveJobApplicationStatusFailure) {
        debugger;
        var params = { employeeJobID: data.employeeJobID, newStatus: data.status };
        $.FCA.ajaxTools.submitAjaxPostJsonRequest($.FCA.page.links.saveJobApplicationStatusLink, params, onSaveJobApplicationStatusComplete, onSaveJobApplicationStatusFailure);
    };

    return dataAccess;
})();