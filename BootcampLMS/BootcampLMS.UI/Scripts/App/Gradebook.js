(function ($) {

    var url = '/api/gradebook';

    var hide = function ($td) {
        $('.display', $td).removeClass('hidden');
        $('.editor', $td).addClass('hidden');
    };

    $('td.grade-editor').click(function () {

        var $td = $(this);
        var assignmentTrackerId = $td.attr('data-id');
        console.log(assignmentTrackerId);

        $('.display', $td).addClass('hidden');
        $('.editor', $td).removeClass('hidden');
    });

    $(".grade-editor .cancel").click(function (e) {
        e.stopPropagation();
        var $td = $(this).closest('td');
        hide($td);
    });

    $(".grade-editor .save").click(function (e) {
        e.stopPropagation();

        var $td = $(this).closest('td');
        var at = {
            Id: $td.attr('data-id'),
            AssignmentId: $td.attr('data-assignment-id'),
            RosterId: $td.attr('data-roster-id'),
            EarnedPoints: $("input", $td).val()
        }

        console.log(at);

        $.ajax({
            url: url,
            method: 'POST',
            data: at,
            dataType: 'json'
        })
			.success(function (data) {
			    var possible = $td.attr('data-possible-points');
			    possible = parseInt(possible, 0);
			    var percentString = data.EarnedPoints / possible * 100;
			    $('.display', $td).text(percentString.toFixed(1) + '%');
			    hide(e);
			})
			.error(function () {
			    alert("An error occurred talking to the server!");
			});

        hide($(this).closest('td'));
    });

}(jQuery));