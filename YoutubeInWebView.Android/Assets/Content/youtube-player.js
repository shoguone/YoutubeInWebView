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
            disablekb: 1,
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

    switchPlayPauseButtons(event.data == YT.PlayerState.PLAYING);

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

///////////////////
// buttons handling
///////////////////

const _playerWrapperEl = document.getElementById('player-wrapper');

const btnPlay = document.getElementById('button-play');
const btnPause = document.getElementById('button-pause');
const btnNext = document.getElementById('button-next');
const btnPrev = document.getElementById('button-prev');

let timer = null;

_playerWrapperEl.addEventListener('mouseenter', () => {
    _playerWrapperEl.classList.add('is-showed-controls');
    _playerWrapperEl.classList.remove('no-displayed');
});

_playerWrapperEl.addEventListener('mouseleave', () => {
    clearTimeout(timer);
    timer = setTimeout(() => {
        _playerWrapperEl.classList.add('no-displayed');
        _playerWrapperEl.classList.remove('is-showed-controls');
    }, 3000);
});

btnPlay.addEventListener('click', onBtnPlay);
btnPause.addEventListener('click', onBtnPause);
btnNext.addEventListener('click', onBtnNext);
btnPrev.addEventListener('click', onBtnPrev);

function onBtnPlay() {
    console.log('btnPlay');
    player.playVideo();
    switchPlayPauseButtons(true);
}

function onBtnPause() {
    console.log('btnPause');
    player.pauseVideo();
    switchPlayPauseButtons(false);
}

function onBtnNext() {
    console.log('btnNext');
    player.nextVideo();
}

function onBtnPrev() {
    console.log('btnPrev');
    player.previousVideo();
}

function switchPlayPauseButtons(isPlaying) {
    if (isPlaying) {
        btnPause.style.display = 'flex';
        btnPlay.style.display = 'none';
    }
    else {
        btnPlay.style.display = 'flex';
        btnPause.style.display = 'none';
    }
}
