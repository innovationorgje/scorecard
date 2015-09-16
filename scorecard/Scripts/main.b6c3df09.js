$(document).ready(function ()
{
    $('.panel-footer-update').hide();

    $('.update-criteria').click(function ()
    {
        $('.panel-footer').show();

        var baseId = $(this).prop('id').replace('upd-', '');
        var id = '#rec-' + baseId;

        $(id + ' .panel-footer').hide();

        // set value of controls
        $('.panel-footer-update #UpdateId').val(baseId);
        $('.panel-footer-update textarea').val('');
        $('.panel-footer-update select').val($(id + ' .current-status').val());

        // set ajax update element
        $('.panel-footer-update form').attr('data-ajax-update', '#panel-body-' + baseId + ' p:last-child');

        // show update panel
        var updatePanel = $('.panel-footer-update');
        $(updatePanel).detach();

        $(id).append(updatePanel);
        $(updatePanel).show();
    });

    $('.cancel-update').click(function ()
    {
        resetUpdateStatusControls();
    });
});

function updateStatusSuccess(arg)
{
    console.debug(arg);
    resetUpdateStatusControls();
}

function resetUpdateStatusControls()
{
    $('.panel-footer-update #UpdateId').val('');
    $('.panel-footer-update textarea').val('');
    $('.panel-footer-update select').val(0);

    $('.panel-footer').show();
    $('.panel-footer-update').hide();
}

function showAjaxFormError(error)
{
    console.debug(error);
}

