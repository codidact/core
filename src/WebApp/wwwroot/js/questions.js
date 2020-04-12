window.addEventListener("load", function () {
    var questions = document.querySelectorAll(".question-date");
    for (var i = 0; i < questions.length; i++) {
        var dateAttribue = questions[i].attributes.getNamedItem('data-date');
        if (dateAttribue) {
            var date = dateAttribue.value;
            questions[i].innerHTML = utcDateToLocalDisplay(date);
        }
    }
});
