$(document).ready(function () {
    var orderElement = document.getElementById('orderId');
    var orderId;
    if (orderElement) {
        orderId = orderElement.value

        var options = {
            "name": "Acme Corp",
            "description": "Buy Green Tea",
            "order_id": orderId,
            "image": "https://example.com/your_logo",
            "prefill": {
                "name": "Gaurav Kumar",
                "email": "gaurav.kumar@example.com",
                "contact": "+919000090000",
            },
            "notes": {
                "address": "Hello World"
            },
            "theme": {
                "color": "#3399cc"
            }
        }
        // Boolean whether to show image inside a white frame. (default: true)
        options.theme.image_padding = false;
        options.handler = function (response) {
            document.getElementById('razorpay_payment_id').value = response.razorpay_payment_id;
            document.getElementById('razorpay_order_id').value = orderId;
            document.getElementById('razorpay_signature').value = response.razorpay_signature;
            document.razorpayForm.submit();
        };
        options.modal = {
            ondismiss: function () {
                console.log("This code runs when the popup is closed");
                window.location.href = window.location.origin + "/failed"
            },
            // Boolean indicating whether pressing escape key
            // should close the checkout form. (default: true)
            escape: true,
            // Boolean indicating whether clicking translucent blank
            // space outside checkout form should close the form. (default: false)
            backdropclose: false
        };
        var rzp = new Razorpay(options);
        //document.getElementById('rzp-button1').onclick = function (e) {
        rzp.open();
        //   e.preventDefault();
        //}
    }
});