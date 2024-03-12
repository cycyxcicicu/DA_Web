var removeVietnameseTones = function (str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
    str = str.replace(/ + /g, " ");
    str = str.trim();
    // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
    return str;
}
const validateEmail = (email) => {
    return String(email)
        .toLowerCase()
        .match(
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        );
};
var toDateView = function (data, char = '/') {
    if (data == null)
        return "";
    for (let i = 0; i < data.length; i++)
        if ((data[i] > '9' || data[i] < '0') && data[i] != '-')  {
            data = data.substr(0, i);
            break;
        }
    let arr = data.split('-');
    if (arr.length < 3)
        return "";
    return to2(arr[2]) + char + to2(arr[1]) + char + to2(arr[0]);
}
var toDateInput = function (data) {
    if (data == null)
        return "";
    let arr = data.split('/');
    if (arr.length < 3)
        return "";
    return to2(arr[2]) + "-" + to2(arr[1]) + "-" + to2(arr[0]);
}
var toDateToServer = function (data) {
    if (data == null)
        return "";
    let arr = data.split('/');
    if (arr.length < 3)
        return "";
    return (arr[2]) + "-" + bo0(arr[1]) + "-" + bo0(arr[0]);
}
var bo0 = (data) => {
    if (data.length > 0)
        return data[data.length - 1];
    return "";
}
var toDateTimeView = function (data, char = '/') {
    if (data == null)
        return "";
    let arr = data.substr(0, 10).split('-');
    if (arr.length < 3)
        return "";
    return data.substr(11, 8) + ' ' + to2(arr[2]) + char + to2(arr[1]) + char + to2(arr[0]);
}
var toDateTimeInput = function (data) {
    if (data == null)
        return "";
    let arr = data.substr(9, 10).split('/');
    if (arr.length < 3)
        return "";
    return to2(arr[2]) + "-" + to2(arr[1]) + "-" + to2(arr[0]) + 'T' + data.substr(0, 8);
}

var to2 = function (data) {
    if (data.length == 1)
        return '0' + data;
    return data;
}
var xoaKhoangTrang = function (data) {
    return data.replace(/ /g, '');
}

function checkImageType(url, success, error) {

    fetch(url)
        .then(response => {
            return response.blob();
        })
        .then(blob => {
            const fileType = blob.type;
            if (fileType.startsWith('image/')) {
                if (success && typeof success == 'function')
                    success()
            } else {
                if (error && typeof error == 'function')
                    error()
            }
        })
        .catch(err => {
            if (error && typeof error == 'function')
                error()
        });
}