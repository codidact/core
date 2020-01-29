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

/**
 * toggleViewEmailPrivacyInfo
 * 
 * This function is called from /Home/Register by
 * Register.cshtml. It displays/hides the email privacy
 * information dialog by manipulating the notice element's
 * display style attribute.
 * 
 * @author Jose Fernando Lopez Fernandez <jflopezfernandez@gmail.com>
 */
const toggleViewEmailPrivacyInfo = () => {
    const widget = document.getElementById("notice-email-privacy-information");
    widget.style.display = (widget.style.display === 'none') ? 'block' : 'none';
};
