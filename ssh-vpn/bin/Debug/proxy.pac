
function FindProxyForURL(url, host) {

    // پاک‌سازی host
    var cleanHost = host.toLowerCase();

    // حذف پورت
    var portIndex = cleanHost.indexOf(':');
    if (portIndex > -1) cleanHost = cleanHost.substring(0, portIndex);

    // حذف نقطه آخر
    if (cleanHost.endsWith('.')) cleanHost = cleanHost.slice(0, -1);

    if (dnsDomainIs(cleanHost, '*.uptvs.com') || shExpMatch(cleanHost, '*.uptvs.com')) return 'DIRECT';
    if (isInNet(cleanHost, '192.168.10.0', '255.255.255.0')) return 'DIRECT';
    if (isInNet(cleanHost, '192.168.1.0', '255.255.255.0')) return 'DIRECT';
    if (dnsDomainIs(cleanHost, '<local>') || shExpMatch(cleanHost, '<local>')) return 'DIRECT';
    if (dnsDomainIs(cleanHost, '*.bale.ai') || shExpMatch(cleanHost, '*.bale.ai')) return 'DIRECT';
    if (dnsDomainIs(cleanHost, '*.kabirmotor.com') || shExpMatch(cleanHost, '*.kabirmotor.com')) return 'DIRECT';
    if (dnsDomainIs(cleanHost, '*.iran.ir') || shExpMatch(cleanHost, '*.iran.ir')) return 'DIRECT';
    if (dnsDomainIs(cleanHost, '*.stts.ir') || shExpMatch(cleanHost, '*.stts.ir')) return 'DIRECT';


    return 'PROXY 127.0.0.1:8080';
}
