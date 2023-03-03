$('td button').each(function () {
    $(this).on('click', function () {
        let domain = this.getAttribute('data-property');
        // has a domain => copy
        // else => post 
        if (domain) {
            navigator.clipboard.writeText(domain)
                .then(() => {
                    alert("Successfully wrote to clipboard")
                })
                .catch(err => {
                    console.error('Failed to write to clipboard: ', err);
                });
        }
    });
});