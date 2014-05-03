calendar = {
    element: null,
    today: null,
    startDate: null,
    endDate: null,
    months: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
    monthNames: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
    dayNames: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
    dayFullNames: ['Mon', 'Tues', 'Wed', 'Thurs', 'Fri', 'Sat', 'Sun'],
    yearsForYearView: 3,
    make: function (input_element) {
        this.element = document.getElementById(input_element);
        this.today = new Date();
        this.startDate = this.today;
        this.setDate(4, 8, this.today.getFullYear(), "year").loadMonthsView();
    },
    setDate: function (day, month, year, length) {
        this.startDate = new Date(year, month, day);
        if (length == "years") {
            year = year + (this.yearsForYearView - 1);
        }
        if (length == "years" || length == "year") {
            month = 11;
        }
        if (length == "years" || length == "year" || length == "month") {
            day = this.getDaysInMonth(month, year);
        }
        if (length == "years" || length == "year" || length == "month" || length == "day") {
            this.endDate = new Date(year, month, day);
        }
        return this;
    },
    getDaysInMonth: function (month, year) {
        return new Date(year, month + 1, 0).getDate();
    },
    getFormattedDate: function (day, month, year) {
        return day.toString() + '/' + this.months[month] + '/' + year.toString();
    },
    loadYearView: function () {
        var table = "<table id='YearView'>";
        table += "<thead><tr><th colspan='3'>Choose a year</th></tr></thead>";
        table += "<tbody><tr>";
        for (var i = 0; i < this.yearsForYearView; i++) {
            var cell = "<td class='year' data-year='" + (this.today.getFullYear() - 1 + i) + "'>" + (this.today.getFullYear() - 1 + i) + "</td>";
            table += cell;
        }
        table += "</tr></tbody></table>";
        this.element.innerHTML = table;
        var yearElements = document.getElementsByClassName('year');
        for (var i = 0; i < yearElements.length; i++) {
            yearElements[i].onclick = function () { calendar.setDate(1, 1, this.getAttribute("data-year"), "year").loadMonthsView(); };
        }
    },
    loadMonthsView: function () {
        var table = "<table id='monthsView'><thead id='revert'>";
        table += "<tr><th colspan='3'>" + this.startDate.getFullYear() + " (click here to choose a year)</th></tr>";
        table += "</thead><tbody><tr>";
        for (var i = 0; i < this.monthNames.length; i++) {
            var cell = "<td class='month' data-month='" + i + "'>" + this.monthNames[i] + "</td>";
            (i % 3 == 0 && i != this.monthNames.length && i != 0) ? table += "</tr><tr>" + cell : table += cell;
        }
        table += "</tr></tbody></table>";
        this.element.innerHTML = table;
        document.getElementById('revert').onclick = function () { calendar.loadYearView(); };
        var monthsElements = document.getElementsByClassName('month');
        for (var i = 0; i < monthsElements.length; i++) {
            monthsElements[i].onclick = function () { calendar.setDate(1, this.getAttribute("data-month"), calendar.startDate.getFullYear(), "month").loadMonthView(); };
        }
    },
    loadMonthView: function () {
        var table = "<table id='monthView'><thead id='revert'>";
        table += "<tr><th colspan='7'>" + this.monthNames[this.startDate.getMonth()] + " " + this.startDate.getFullYear() + " (click here to choose a month)</th></tr>";
        table += "</thead><tbody><tr>";
        for (var i = 0; i < this.dayNames.length; i++) {
            table += "<th>" + this.dayNames[i] + "</th>";
        }
        table += "</tr><tr>";
        var daysInMonth = this.getDaysInMonth(this.startDate.getMonth(), this.startDate.getFullYear());
        var startDay = ((new Date(this.startDate.getFullYear(), this.startDate.getMonth(), 1).getDay() - 1) % 7);
        if (startDay < 0) { startDay = 6; }
        for (var i = 1; i <= daysInMonth + startDay; i++) {
            if (i <= startDay) {
                table += "<td></td>";
            } else {
                var cell = "<td class='day'>" + (i - startDay) + "</td>";
                (i % 7 == 1 && i != 1) ? table += "</tr><tr>" + cell : table += cell;
            }
        }
        var rowCount = (table.split("<tr>").length - 1) - 2;
        for (var i = 0; i < ((7 * rowCount) - (daysInMonth + startDay)); i++) {
            table += "<td></td>";
        }
        table += "</tr></tbody></table>";
        this.element.innerHTML = table;
        document.getElementById('revert').onclick = function () { calendar.loadMonthsView(); };
        var monthsElements = document.getElementsByClassName('day');
        /*for (var i = 0; i < monthsElements.length; i++) {
            monthsElements[i].onclick = function () { calendar.setDate(this.innerHTML, calendar.startDate.getMonth(), calendar.startDate.getFullYear(), "day").loadDayView(); };
        }*/
    }
}