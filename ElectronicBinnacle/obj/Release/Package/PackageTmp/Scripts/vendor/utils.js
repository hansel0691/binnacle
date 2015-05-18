$(document).ready(function() {
    var $primaryNav = $('#primaryTopNav');
    var $nav = $('#topNav');
    var $window = $(window);

    var scrollTop = function($element) {
        return $element.offset().top - $window.scrollTop();
    }

    if ($primaryNav.length > 0 && $nav.length > 0) {
        var topSpace = 25;

        $window.on('scroll resize ', function() {
            var columnOffsetTop = scrollTop($primaryNav);

            if (topSpace + columnOffsetTop < 0) {
                $nav.css({
                    'position': 'fixed',
                    'top': 0 + 'px'
                });
            } else {
                $nav.css({
                    'position': 'absolute',
                    'top': topSpace + 'px'
                });
            }
        });
    }
});


function getDateMls(dmy) {
    if (!dmy) return 0;
    var d = dmy.split('/');
    if (d.length != 3)
        d = dmy.split('.');
    var date = new Date(Number(d[2]), Number(d[1]) - 1, Number(d[0]));
    return date.getTime() - date.getTimezoneOffset() * 60 * 1000;
}
function getDateStr(mls, separator) {
    if (!mls || mls < 0) return '';
    if (!separator) separator = '/';
    var date = new Date(mls);
    return [prettyTime(date.getUTCDate()), prettyTime(date.getUTCMonth() + 1), date.getUTCFullYear()].join(separator);
}
function getTimeMls(hm) {
    if (!hm) return 0;
    var time = hm.split(':');
    return Number(time[0]) * 60 * 60 * 1000 + Number(time[1]) * 60 * 1000;
}
function getTimeStr(mls) {
    if (!mls || mls < 0) return '';
    return [prettyTime(Math.floor(mls / (60 * 60 * 1000)) % 24), prettyTime(Math.floor(mls / (60 * 1000)) % 60)].join(':');
}
function getDateTimeStr(mls, separator) {
    if (!mls || mls < 0) return '';
    return [getDateStr(mls, separator), getTimeStr(mls)].join(' - ');
}
function getNowMls() {
    var date = new Date();
    return Date.now() - date.getTimezoneOffset() * 60 * 1000;
}
function prettyTime(n) {
    return (n < 10 ? '0' + n : n);
}
function getTimeObj(number) {
    if (!number) return { minute: "", hour: ""};
    var hd = getTimeStr(number).split(':');
    return { minute: hd[1], hour: hd[0] };
}
function dateDif(date1, date2) {
    var result = Math.abs(date1 - date2);
    var days = parseInt(result / (24 * 60 * 60 * 1000));
    var hours = parseInt((result % (24 * 60 * 60 * 1000)) / (60 * 60 * 1000));
    var minutes = (parseInt(((result % (24 * 60 * 60 * 1000)) % (60 * 60 * 1000)) / (60 * 1000)) + 1);


    var text = '';
    if (days)
        text += days + ' d\u00EDa' + pluralize(days) + (hours && minutes ? ', ' : (hours || minutes ? ' y ' : ''));
    if (hours)
        text += hours + ' hora' + pluralize(hours) + (minutes ? ' y ' : '');
    if (minutes)
        text += minutes + ' minuto' + pluralize(minutes);

    if (date1 - date2 < 0)
        {text = text + " antes de tiempo.";}
    else
        text = text + " despu\u00E9s de tiempo.";
    return text;
}
function pluralize(number) {
    return (number > 1 ? 's' : '');
}

function round(number, place) {
    var x = new Number(number);
    var p = new Number(place);
    if (Number.isNaN(x) || Number.isNaN(p))
        return null;
    return Math.round(x * Math.pow(10, p)) / Math.pow(10, p);
}