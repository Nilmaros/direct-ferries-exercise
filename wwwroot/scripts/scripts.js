jQuery(document).ready(function () {
    $("#buttonclicked").click(function () {

        var radioValue = $("input[name='order_type']:checked").val();
        $("ul li").remove();
        $.get("./getallusers/" + radioValue, function (data) {
            $.each(data, function (a, b) {
                $(".result").append("<li>" + b.firstName + " " + b.surname + " has a score of: " + b.score + "</li>");
            });
        });
    });
});