y = (new Date()).getFullYear(); //預設今年，yyyy
weekends = []; //六日
old_holidays = [], new_holidays = []; //old為DB中舊的holidays
finalSelected='1/1/'+y; //在postback時，最後選的在下次執行時會被預設選散，須要額外刪除

$(document).ready(function () {
    $('#reset_btn').click(function () {
        //多選的datespicker
        $('.full-year').multiDatesPicker('destroy');        
        new_holidays = weekends;
        console.log(new_holidays);
        initi();
        $('.full-year').multiDatesPicker('removeDates', finalSelected); //重建
        return false;
    });

    $('#save_btn').click(function () {
        if (confirm("儲存後將覆蓋原先資料")) {
            save_holidays();
        }    
        return false;
    });
    
    //新增prototype，增加天數
    Date.prototype.addDays = function (days) {
        this.setDate(this.getDate() + parseInt(days));
        return this;
    }

    //新增prototype，回傳成指定格式
    Date.prototype.ToString = function () {
        return (this.getMonth() + 1) + '/' + this.getDate() + '/' + this.getFullYear();
    }

    var s ='';
    for (var i = y - 1; i <= y + 3; i++) {
        s += '<option value=' + i + '>' + i + '年</option>';
    }
    $('#year_sel').html(s).val(y);
    $('#year_sel').change(function () {
        y = $('#year_sel').val();
        $('.full-year').multiDatesPicker('destroy');
        set_weekends();
    });

    set_weekends();          
});

function set_weekends() {
    weekends = [];    

    var firstDay = new Date('1/1/' + y); //該年第一天
    var firstWeekend = new Date(); //該年第一個假日

    while (true) {
        if (firstDay.getDay() == 0 || firstDay.getDay() == 6) //若getDay=0 => 禮拜日，若getDay=6 =>禮拜六
        { firstWeekend = firstDay; break; }
        firstDay.addDays(1);
    }
    //firstDay == firstWeekend == 第一個六日

    var day = new Date(firstWeekend);
    //若day=>禮拜日，day push to weekend，並將day+6
    if (day.getDay() == 0) { weekends.push(day.ToString()); day.addDays(6); }

    //day必為該年第一個禮拜六

    while (true) {
        weekends.push(day.ToString()); //將禮拜六加入week
        day.addDays(1); //day +1 => 禮拜日
        if (day.getFullYear() != y) { break; } //若+1後已是隔年，則break
        weekends.push(day.ToString());
        day.addDays(6); //下一週的禮拜六
        if (day.getFullYear() != y) { break; }
    }
    get_holidays();
}

function initi() {
    //初始化multiDatesPicker
    $('.full-year').multiDatesPicker({
        minDate: '1/1/' + y,
        maxDate: '12/31/' + y,
        addDates: new_holidays,
        numberOfMonths: [1, 4],
        defaultDate: new_holidays[0],
        altField: '#pickDate_text',
        onSelect: function (dateText, inst) {
            finalSelected = dateText;
            //console.log(finalSelected);
            var t = $('#pickDate_text').val();
            t = t.replace(/ /g, '');
            new_holidays = t.split(',');
            show_days();
        }
    });

    //潤年判斷
    if (((y % 4 == 0) && !(y % 100 == 0)) || (y % 400 == 0)) {
        $('#total_days_lab').text(366);
    }
    else { $('#total_days_lab').text(365); }
    show_days();
}

function get_holidays() {
    //呼叫API_getHolidays.aspx，取得已在db的holidays
    //需傳入年份

    var strURL = 'http://localhost:8082/HR/API_getHolidays.aspx';
    $.ajax({
        type: 'POST',
        url: strURL,
        data: {
            year:y
        },
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        crossDomain: true,
        dataType: 'json',
        timeout: 20000,
        success: function (data) {
            console.log(data);
            old_holidays = [];
            if ((data.Holidays).length == 0) {
                new_holidays = weekends;
            }
            else {
                $(data.Holidays).each(function (index, value) {
                    old_holidays.push(value.Date);
                });
                new_holidays = old_holidays;
            }
            initi();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        }
    });
}

function show_days() {
    $('#holiday_days_lab').text(new_holidays.length);
    $('#work_days_lab').text(parseInt($('#total_days_lab').text()) - parseInt($('#holiday_days_lab').text()));
}

function save_holidays() {
    //儲存holidays

    var strURL = 'http://localhost:8082/HR/API_setHolidays.aspx';

    //比對新舊的holidays，若重覆則刪除，不需要刪了又新增
    var oldH = old_holidays, newH = new_holidays;
    $(oldH).each(function (i, value) {
        if ($.inArray(value, newH) != -1) {
            oldH = $.grep(oldH, function (v) { return v != value; });
            newH = $.grep(newH, function (v) { return v != value; });
        }
    });

    //呼叫API_setHolidays，新增holidays    
    $.ajax({
        type: 'POST',
        url: strURL,
        data: {
            old_dates: oldH.toString(),
            new_dates: newH.toString()
        },
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        crossDomain: true,
        dataType: 'json',
        timeout: 20000,
        success: function () {
            alert('儲存成功');
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        }
    });
}
