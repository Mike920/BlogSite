//apply ng model to form elements based on element id
$('#upload-form').find("[name]").each(function() {
    $(this).attr('ng-model', 'model.' + $(this).attr("id"));
});
//bind model to id hidden field (required by remote validation)
$('#upload-form').find("[name='Id']").val('{{model.Id}}');