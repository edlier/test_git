//共用模組，需要datepicker的textbox，使用css datepicker

$(document).ready(function () {
    date_initi();
});

function date_initi() {
    try {
        var opt = {
            'scrollDefault': 'now',
            'disableTextInput': true,
            'minTime': '00:00',
            'maxTime': '23:30',
            'disableTimeRanges': [],
            'timeFormat': 'H:i'
        }

        $('.timepicker').timepicker(opt);
        $('.timepicker').css({ 'width': '4.5em', 'text-align': 'center', 'font-size': '14pt' });
    }
    catch (err) { console.log(err); }

    var opt = {
        dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
        dayNamesMin: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        monthNamesShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        prevText: "last month",
        nextText: "next month",
        weekHeader: "week",
        showMonthAfterYear: true,
        dateFormat: "yy-mm-dd",
        defaultDate: Date()//,
        //minDate: "-14",
        //maxDate: "+14"
    };
    $('.datepicker').datepicker(opt);
    $('.datepicker').css({ 'width': '5.5em', 'text-align': 'center', 'font-size': '14pt' });
}