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
            'onStateChange': onPlayerStateChange,
            'onPlaybackQualityChange': onPlaybackQualityChange,
            'onPlaybackRateChange': onPlaybackRateChange,
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

function onPlaybackQualityChange(event) {
    console.log('onPlaybackQualityChange', JSON.stringify(arguments))
    jsBridge.onPlaybackQualityChange(event.data);
}

function onPlaybackRateChange(event) {
    console.log('onPlaybackRateChange', JSON.stringify(arguments))
    jsBridge.onPlaybackRateChange(event.data);
}

function onPlayerError(event) {
    console.log('onPlayerError', JSON.stringify(arguments))
    jsBridge.onPlayerError(event.data);
}
