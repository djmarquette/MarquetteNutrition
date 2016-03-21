/*!
* jQuery and JavaScript functions for MNF4 Site
* http://www.marquettenutrition.com/
* 
* Date: Fri April 12, 2013
*/
$(document).ready(function () {
    var $ShowToolTip = (function (widget) {
        var toolTip = document.getElementById(widget);
        toolTip.title = toolTip.title;});
    $(".accordion").accordion({ collapsible: true, heightStyle: "content", active: false });
    $("#menu").menubar({
        autoExpand: true, buttons: true, menuIcon: true, navigation: true,
        position: { within: $('#frame').add(window).first() }
    });
    $("#menu li a").click(function () {
        window.location = $(this).attr('href');
        return false;
    });
});