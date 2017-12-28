
$('#file').change(function () {
    var fileName = $(this).val();
    var label = fileName.replace(/\\/g, '/').replace(/.*\//, '');
    $('input[name=PathPicture]').val(label);
});