CKEDITOR.editorConfig = function (config) {
	config.extraPlugins = 'autogrow,autoembed';
	config.autoEmbed_widget = 'customEmbed';


	//config.uiColor = '#4afaa3';

	config.extraPlugins = 'autogrow';
	config.autoGrow_minHeight = 200;
	config.autoGrow_maxHeight = 600;
	config.autoGrow_bottomSpace = 50;
	config.removePlugins = 'elementspath';
	config.height = '3em';


	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowserBrowseUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Assets/Admin/plugins/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = '/Assets/Admin/plugins/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/Assets/Admin/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = '/Images';
	config.filebrowserFlashUploadUrl = '/Assets/Admin/plugins//ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/Assets/Admin/plugins/ckfinder/');

	config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
	];

	config.removeButtons = 'Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Templates,Cut,Copy,Paste,PasteText,PasteFromWord,Unlink,Link,BidiLtr,BidiRtl,Language,Anchor,Flash,HorizontalRule,PageBreak,ShowBlocks,About,Replace';
};