$.validator.addmethod("filesize", function (value, element, param) {
    return this.optional(element) || element.files[0].size <= param;
});