//prova creazione xml
function genXML(b){
    const convert = import('xml-js')
    const obj = b;
    const xml = convert.js2xml(obj, { compact: true })
}

