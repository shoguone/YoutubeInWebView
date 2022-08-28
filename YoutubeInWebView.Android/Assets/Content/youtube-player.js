var player;

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
function onYouTubeIframeAPIReady() {
    jsBridge.onApiReady();
}

function setUpPlayer(videoId, w, h) {
    player = new YT.Player('player', {
        height: h,
        width: w,
        videoId: videoId,       // 'junBvKGZCDc'
        playerVars: {           // https://developers.google.com/youtube/player_parameters.html?playerVersion=HTML5
            color: 'white',
            controls: 0,
            modestbranding: 1,
            rel: 0,
            showinfo: 0,
            loop: 0,
            fs: 0,
            hl: 'en',
            playsinline: 1,
            enablejsapi: 1,
        },
        events: {               // https://developers.google.com/youtube/iframe_api_reference#Events
            'onReady': onPlayerReady,
            'onPlaybackQualityChange': onPlayerPlaybackQualityChange,
            'onStateChange': onPlayerStateChange,
            'onError': onPlayerError,
        }
    });
}

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    console.log('onPlayerReady', JSON.stringify(arguments))
    jsBridge.onPlayerReady();
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
function onPlayerStateChange(event) {
    console.log('onPlayerStateChange', JSON.stringify(arguments))
    jsBridge.onPlayerStateChange(event.data);
}

function onPlayerPlaybackQualityChange(data) {
    console.log('onPlayerPlaybackQualityChange', JSON.stringify(arguments))
}

function onPlayerError(data) {
    console.log('onPlayerError', JSON.stringify(arguments))
}
