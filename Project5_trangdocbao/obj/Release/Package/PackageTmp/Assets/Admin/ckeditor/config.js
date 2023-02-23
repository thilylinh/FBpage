/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */
CKEDITOR.editorConfig = function (config) {
	config.extraPlugins = 'autogrow,autoembed';
	config.autoEmbed_widget = 'customEmbed';

	//config.uiColor = '#4afaa3';

	config.autoGrow_minHeight = 50;
	//config.autoGrow_maxHeight = 600;
	config.autoGrow_bottomSpace = 50;
	config.removePlugins = 'elementspath';

	//config.height = '3em';

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
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'insert', groups: ['insert'] },
		'/',
		{ name: 'links', groups: ['links'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
	];
	config.removeButtons = 'ExportPdf,Preview,Print,Save,NewPage,Templates,Cut,Copy,Paste,PasteText,PasteFromWord,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,HiddenField,RemoveFormat,Blockquote,CreateDiv,BidiLtr,BidiRtl,Language,Anchor,Unlink,Link,Table,HorizontalRule,Smiley,PageBreak,ShowBlocks,About';
};