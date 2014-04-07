//Tooltip
$('a').tooltip('hide');

//Popover
//$('.popover-pop').popover('hide');

//Collapse
$('#myCollapsible').collapse({
  toggle: false
})

//Dropdown
$('.dropdown-toggle').dropdown();


// Retina Mode
function retina(){
  retinaMode = (window.devicePixelRatio > 1);
  return retinaMode;
}


//$(document).ready(function () {
//  pieCharts();
//});


// Date Time
setInterval(function(){ 
  date = '<span class="big">' + moment().format('MMMM Do YYYY') + '</span><span>' + moment().format('ddd hh:mm:ss a') + '</span>'
  $("#date-time").html(date)
}, 1000);



//function pieCharts() {
//  $(function () {
//    //create instance
//    $('.pie_chart_1').easyPieChart({
//      animate: 2000,
//      barColor: '#74b749',
//      trackColor: '#dddddd',
//      scaleColor: '#74b749',
//      size: 180,
//      lineWidth: 8,
//    });
//    //update instance after 5 sec
//    setTimeout(function () {
//      $('.pie_chart_1').data('easyPieChart').update(69);
//    }, 5000);
//    setTimeout(function () {
//      $('.pie_chart_1').data('easyPieChart').update(20);
//    }, 15000);
//    setTimeout(function () {
//      $('.pie_chart_1').data('easyPieChart').update(78);
//    }, 27000);
//    setTimeout(function () {
//      $('.pie_chart_1').data('easyPieChart').update(52);
//    }, 39000);
//    setTimeout(function () {
//      $('.pie_chart_1').data('easyPieChart').update(89);
//    }, 45000);
//  });
//}

//Resize charts and graphs on window resize
//$(document).ready(function () {
//  $(window).resize(function(){
//    pieCharts();
//  });
//});

//Tiny Scrollbar
$('#scrollbar-three').tinyscrollbar();
function postSuccess() {
    jQuery('.blog-de-comment-post h4 .post-input, .blog-de-comment-post h4 .post-input-text').val('');
    jQuery('#message').html('');
    jQuery('#message').append('Thanks, your email was sent.').addClass("green");
    jQuery('#message').css('display', 'block').fadeIn(5000).fadeOut(25000);;
}

function postFailed() {
    jQuery('#message').html('');
    jQuery('#message').append('Error sending your message. Please try again later.').addClass("red");
    jQuery('#message').css('display', 'block').fadeIn(5000).fadeOut(25000);;
}

function OnBegin() {
    jQuery('#message').html('');
}
function OnSuccess(data) {
    jQuery('#message').html(showMessage('success', 'Success', data));
}
function OnFailure(request, error) {
    jQuery('#message').html(showMessage('error', 'Error', error));
}
function OnComplete(request, status) {
   // jQuery('#message').html(showMessage('success', 'Success', status));
}

function showMessage(messageType, title, message)
{
    var message = '<div class="alert alert-block alert-' + messageType + ' fade in">';
    message =+'<button type="button" class="close" data-dismiss="alert">×</button>';
    message =+'<h4 class="alert-heading">' + title + '!</h4><p>' + message + '</p></div>';
    return message;
}
$(document).ready(function () {
    // Support for AJAX loaded modal window.
    // Focuses on first input textbox after it loads the window.
    $('[data-toggle="modal"]').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        if (url.indexOf('#') == 0) {
            $(url).modal('open');
        } else {
            $.get(url, function (data) {
                $('<div class="modal hide fade">' + data + '</div>').modal();
            }).success(function () { $('input:text:visible:first').focus(); });
        }
    });
});