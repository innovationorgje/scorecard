$(document).ready(function()
{
    $('.panel-footer-update').hide();
    $('.update-criteria').click(function ()
    {
        $('.panel-footer').show();

        var baseId = $(this).prop('id').replace('upd-', '');
        var id = '#rec-' + baseId;

        $(id + ' .panel-footer').hide();

        // set value of controls
        $('.panel-footer-update input[type=hidden]').val(baseId);
        $('.panel-footer-update textarea').val('');
        $('.panel-footer-update select').val($(id + ' .current-status').val());

        // show update panel
        var updatePanel = $('.panel-footer-update');
        $(updatePanel).detach();

        $(id).append(updatePanel);
        $(updatePanel).show();
    });

    $('.submit-update').click(function ()
    {
        var id = $('.panel-footer-update input[type=hidden]');

        $.ajax({
            type: "POST",
            url: "/Criteria/UpdateStatus/" + id,
            data: $('.panel-footer-update form').serialize(),
            datatype: "html",
            success: function (update)
            {
                $('#rec-' +  id + ' .panel-body').append(update);
            }
        });
    });

    $('.cancel-update').click(function()
    {
        $('.panel-footer').show();
        $('.panel-footer-update').hide();
    });
})
