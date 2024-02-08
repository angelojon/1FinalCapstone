$(document).ready(function () {
    // Hide all product descriptions
    $('.product-description').hide();

    // Set the initial active radio button and show its corresponding description
    var initialColor = 'red'; // Change this value to the desired initial color
    $('input[data-image="' + initialColor + '"]').prop('checked', true);
    $('.left-column img[data-image="' + initialColor + '"]').addClass('active');
    $('.product-description[data-description="frame-' + initialColor + '"]').show();

    $('.color-choose input').on('click', function () {
        var headphonesColor = $(this).attr('data-image');

        $('.active').removeClass('active');
        $('.left-column img[data-image=' + headphonesColor + ']').addClass('active');
        $(this).addClass('active');

        // Hide all product descriptions
        $('.product-description').hide();

        // Show the corresponding product description based on the selected radio button
        $('.product-description[data-description="frame-' + headphonesColor + '"]').show();
    });

    document.getElementById('home-link').addEventListener('click', function (event) {
        var confirmMessage = "Are you sure you want to go to the Home page? Your bike builder data will not be saved.";

        if (!confirm(confirmMessage)) {
            event.preventDefault();
        }
    });
});
