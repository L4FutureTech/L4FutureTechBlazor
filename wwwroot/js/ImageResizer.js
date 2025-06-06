window.resizeFileInputImage = async function (inputElement, maxWidth, maxHeight, quality) {
    return new Promise((resolve, reject) => {
        if (!inputElement.files || inputElement.files.length === 0) {
            resolve(null);
            return;
        }
        var file = inputElement.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = new Image();
            img.onload = function () {
                var canvas = document.createElement('canvas');
                var ctx = canvas.getContext('2d');
                var ratio = Math.min(maxWidth / img.width, maxHeight / img.height);
                canvas.width = img.width * ratio;
                canvas.height = img.height * ratio;
                ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
                var dataUrl = canvas.toDataURL('image/jpeg', quality);
                resolve(dataUrl.split(',')[1]);
            };
            img.onerror = function (err) {
                reject(err);
            };
            img.src = e.target.result;
        };
        reader.onerror = function (err) {
            reject(err);
        };
        reader.readAsDataURL(file);
    });
};
