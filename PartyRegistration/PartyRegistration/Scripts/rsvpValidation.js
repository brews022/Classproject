$(document).ready(function() {
    $('#rsvpForm').validate({
        rules: {
            Name: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Phone: {
                required: true
            },
            FavoriteGame: {
                required: true,
                minlength: 3
            },
            WillAttend: {
                required: true
            }
        },
        messages: {
            Name: "Enter your name",
            Email: {
                required: "Enter your email address",
                email: "That's not a format for email I'm aware of..."
            },
            Phone: "Enter your phone number",
            FavoriteGame: {
                required: "Tell us your favorite game",
                minlength: $.validator.format("I don't know of any game with less than {0} characters...")
            },
            WillAttend: "We need to know if you will attend!"
        }
    });
});