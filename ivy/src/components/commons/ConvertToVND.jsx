function ConvertToVND(params) {
    if(typeof params ==="number"){
    const VND = params.toLocaleString('de-DE');
    return VND;
    }
    return null;
}
export default ConvertToVND