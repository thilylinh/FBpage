$('td button').each(function () {
    $(this).on('click', function () {
        // Store the contents of the clicked cell
        let domain = this.getAttribute('data-property');
        navigator.clipboard.writeText(domain)
            .then(() => {
                alert("Successfully wrote to clipboard")
            })
            .catch(err => {
                console.error('Failed to write to clipboard: ', err);
            });
        // Do something
    });
});
