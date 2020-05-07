/*
 * Header slides
 ----------------
 * This code powers the 'header slides', which are at the core of mobile nav
*/

const headerSlideTriggers = document.querySelectorAll('[data-trigger-header-slide]');

for (let i = 0; i < headerSlideTriggers.length; i++) {
    const trigger = headerSlideTriggers.item(i);
    headerSlideTriggers[i].addEventListener('click', (e) => {
        const headerSlide: HTMLElement = document.querySelector(trigger.getAttribute('data-trigger-header-slide'));

        headerSlide.classList.toggle('is-active');
        this.classList.toggle('is-active');

        // Position header slide appropriately relative to
        // trigger.
        const rect = this.getBoundingClientRect();
        headerSlide.style.top = `${(rect.top + rect.height)}px`;
        headerSlide.style.right = `${(document.body.clientWidth - rect.right)}px`;

        // Prevent navigation
        e.preventDefault();
    });
}

/*
 * Date/Time functions
 ----------------
 * Utilities to handle dates
*/
function utcDateTimeToLocalDisplay(date): string {
    const dateInstance = new Date(date);
    const dateOptions: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'numeric', day: 'numeric' };
    const timeOptions: Intl.DateTimeFormatOptions = { hour12: false };
    const locale = navigator.language;

    return `${dateInstance.toLocaleDateString(locale, dateOptions)} ${dateInstance.toLocaleTimeString(locale, timeOptions)}`;
}

/**
 * Switches all dates from UTC to local client date
 */
window.addEventListener('load', () => {
    const dateElements = document.querySelectorAll('.live-date');
    for (let i = 0; i < dateElements.length; i++) {
        const dateElement = dateElements.item(i);
        const dateAttribue = dateElement.attributes.getNamedItem('data-date');
        if (dateAttribue) {
            const date = dateAttribue.value;
            dateElement.innerHTML = utcDateTimeToLocalDisplay(date);
        }
    }
});
