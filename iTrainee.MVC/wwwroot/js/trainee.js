    var follower = document.getElementById('slider-follow');
    var follower_val = document.getElementById('slider-val');
    var slider = document.getElementById('frame-slider');

    var updateFollowerValue = function (val) {
        follower_val.innerHTML = val;
        follower.style.left = val + '%';
    };

if (slider != null) {
    updateFollowerValue(slider.value);
}

var GetSubTopicList = function (UserId, TopicId, id) {
    var UserPath = "/Trainee/Home/SubTopicList?topicId=" + TopicId + "&userId=" + UserId;
    $("#SubTopic").load(UserPath, function () {
    });
    var firstTopic = $('.topic-name').first();
    $(firstTopic).addClass("topic-name-on-select");
    var someElements = document.querySelectorAll('.topic-name-on-select');
    $(someElements).removeClass("topic-name-on-select");
    $('#' + id).addClass('topic-name-on-select');
};

$("#SaveProgress").click(function () {
    var a = $("#ProgressForm").serialize();
    $.ajax({
        type: "POST",
        url: "/Trainee/Home/UpdateDailyprogress",
        data: a,
        success: function () {
            $("#myModal1").modal("hide");
        },
        error: function () {
            alert("Unable to save/update data");
        },
    });
});

function GetDailyProgress(SubTopicId, UserId, Role) {
    var UserPath = "/Trainee/Home/UpdateDailyProgress?userId=" + UserId + "&subTopicId=" + SubTopicId;
    $("#myModalBodyDiv1").load(UserPath, function () {
        $("#myModal2").modal("show");

        if (Role == "Mentor") {
            $("#TraineeComments").prop("disabled", true);
            $("#MentorComments").prop("disabled", false);
            $(".Percentage").prop("disabled", true);
            $("#Status").prop("disabled", true);
        }
    });
};