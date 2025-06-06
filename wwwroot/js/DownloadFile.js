window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

window.downloadFileFromStreamExcelAndCSV = (filename, base64) => {
    let mimeType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    if (filename.toLowerCase().endsWith('.csv')) {
        mimeType = 'text/csv';
    }
    const link = document.createElement('a');
    link.download = filename;
    link.href = `data:${mimeType};base64,${base64}`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}