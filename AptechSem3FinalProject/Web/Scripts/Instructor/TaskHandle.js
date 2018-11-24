var TaskProcess = (function () {
    const taskItem ='<li> <a href="#"> <h3> Design some buttons <small class="pull-right">20%</small> </h3> <div class="progress xs"> <div class="progress-bar progress-bar-aqua" style="width: 20%" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100"> <span class="sr-only">20% Complete</span> </div> </div> </a> </li>';
    var notificationArea = $(".tasks-menu .task-list");
    var taskCounterArea = $('#taskCounter');

    function generateInProgressTask(str, taskId) {

        return '<li data-task-id="' + taskId + '"> <a href="#"> <h3>' + str + '<i class="fa fa-spinner fa-spin pull-right" style="font-size:24px"></i> </h3> </a> </li>';
    }

    function generateDoneTask(str) {
        return '<li> <a href="#"> <h3>' + str + '<i class="fa fa-check pull-right" style="font-size:24px; color:mediumspringgreen"></i> </h3> </a> </li>';
    }
    return {
        addTask: function (taskName) {
            var uniqueId = new Date().getMilliseconds();
            var htmlAsString = generateInProgressTask(taskName, uniqueId);
            notificationArea.append(htmlAsString);
            taskCounterArea.text(notificationArea.find('li').length);
            return uniqueId;
        },

        updateTask: function (taskName, taskId) {            
            var htmlAsString = generateDoneTask(taskName);
            var taskEl = $(".tasks-menu .task-list li[data-task-id=" + taskId + "]");
            taskEl.replaceWith(htmlAsString);
        },

        deleteAll: function () {
            notificationArea.empty();
            taskCounterArea.text("");
        }
    }
})();