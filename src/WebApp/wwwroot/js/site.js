/*
 * Header slides
 ----------------
 * This code powers the "header slides", which are at the core of mobile nav
*/

const headerSlideTriggers = document.querySelectorAll("[data-trigger-header-slide]");

for (let i = 0; i < headerSlideTriggers.length; i++) {
    headerSlideTriggers[i].addEventListener("click", function (e) {
        const headerSlide = document.querySelector(this.getAttribute("data-trigger-header-slide"));

        headerSlide.classList.toggle("is-active");
        this.classList.toggle("is-active");

        // Position header slide appropriately relative to
        // trigger.
        const rect = this.getBoundingClientRect();
        hs.style.top = (rect.top + rect.height) + "px";
        hs.style.right = (document.body.clientWidth - rect.right) + "px";

        // Prevent navigation
        e.preventDefault();
    });
}

/*
 * Date/Time functions
 ----------------
 * Utilities to handle dates
*/
function utcDateTimeToLocalDisplay(date) {
    var dateInstance = new Date(date);
    var dateOptions = { year: "numeric", month: "numeric", day: "numeric" };
    var timeOptions = { hour12: false };
    var locale = navigator.language;

    return dateInstance.toLocaleDateString(locale, dateOptions) + " " + dateInstance.toLocaleTimeString(locale, timeOptions);
}

/**
 * Switches all dates from UTC to local client date
 */
window.addEventListener("load", function () {
    var questions = document.querySelectorAll(".live-date");
    for (var i = 0; i < questions.length; i++) {
        var dateValue = questions[i].innerHTML;
        questions[i].innerHTML = utcDateTimeToLocalDisplay(dateValue);
    }
});
