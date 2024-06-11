document.addEventListener('DOMContentLoaded', function () {
    const lienMinhMenuItem = document.getElementById('lien-minh');
    const dauTruongMenuItem = document.getElementById('dau-truong');
    const RuneTerraItem = document.getElementById('rune-terra');
    const TocChienItem = document.getElementById("toc-chien");
    const vaLoMenuItem = document.getElementById('valo');
    const RuinedKingItem = document.getElementById('ruined-king');
    const ConverGenceItem = document.getElementById('convergence');
    const SongofNunuItem = document.getElementById('song-of-nunu');
    const RiotForgeItem = document.getElementById('riot-forge');
    const LoleSportsItem = document.getElementById('lol-esports');
    const ValorantEsportsItem = document.getElementById('valorant-esports');
    const ArcaneItem = document.getElementById('arcane');
    const UniverseItem = document.getElementById('universe');
    const RiotMusicItem = document.getElementById('riot-music');
    const RiotGamesItem = document.getElementById('riot-games');
    const RiotSupportItem = document.getElementById('riot-support');
    const mainImage = document.getElementById('main-image');
    const mainText = document.getElementById('main-text');

    lienMinhMenuItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-23.jpeg';
        mainText.innerText = '';
    });

    dauTruongMenuItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-24.jpeg';
        mainText.innerText = '';
    });
    vaLoMenuItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-25.jpeg';
        mainText.innerText = '';

    });
    RuneTerraItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-26.jpeg';
        mainText.innerText = '';
    });
    TocChienItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-27.jpeg';
        mainText.innerText = '';
    });
    RuinedKingItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-28.jpeg';
        mainText.innerText = '';
    });
    ConverGenceItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-29.jpeg';
        mainText.innerText = '';
    });
    SongofNunuItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-30.jpeg';
        mainText.innerText = '';
    });
    RiotForgeItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-31.jpeg';
        mainText.innerText = '';
    });
    LoleSportsItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-32.jpeg';
        mainText.innerText = '';
    });
    ValorantEsportsItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-33.jpeg';
        mainText.innerText = '';
    });
    ArcaneItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-34.jpeg';
        mainText.innerText = '';
    });
    UniverseItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-38.jpeg';
        mainText.innerText = '';
    });
    RiotMusicItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-35.jpeg';
        mainText.innerText = '';
    });
    RiotGamesItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-36.jpeg';
        mainText.innerText = '';
    });
    RiotSupportItem.addEventListener('mouseover', function () {
        mainImage.src = '../Pictures/Backgrounds/Pic-37.jpeg';
        mainText.innerText = '';
    });

    lienMinhMenuItem.addEventListener('mouseout', resetMainImageAndText);
    dauTruongMenuItem.addEventListener('mouseout', resetMainImageAndText);
    vaLoMenuItem.addEventListener('mouseout', resetMainImageAndText);
    RuneTerraItem.addEventListener('mouseout', resetMainImageAndText);
    TocChienItem.addEventListener('mouseout', resetMainImageAndText);
    RuinedKingItem.addEventListener('mouseout', resetMainImageAndText);
    ConverGenceItem.addEventListener('mouseout', resetMainImageAndText);
    SongofNunuItem.addEventListener('mouseout', resetMainImageAndText);
    RiotForgeItem.addEventListener('mouseout', resetMainImageAndText);
    LoleSportsItem.addEventListener('mouseout', resetMainImageAndText);
    ValorantEsportsItem.addEventListener('mouseout', resetMainImageAndText);
    ArcaneItem.addEventListener('mouseout', resetMainImageAndText);
    UniverseItem.addEventListener('mouseout', resetMainImageAndText);
    RiotMusicItem.addEventListener('mouseout', resetMainImageAndText);
    RiotGamesItem.addEventListener('mouseout', resetMainImageAndText);
    RiotSupportItem.addEventListener('mouseout', resetMainImageAndText);




    function resetMainImageAndText() {
        mainImage.src = '../Pictures/IconGames/Icon 13.jpeg';
        mainText.innerText = 'Huyền Thoại Runeterra - Bản Mở Rộng Mới - Con Đường Nguyện Ước';
    }
});
