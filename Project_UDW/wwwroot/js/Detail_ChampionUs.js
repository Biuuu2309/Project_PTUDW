const DTCVideoSkills = {
    noitai: {
        nameDTCskillvideo: 'ĐƯỜNG KIẾM TUYỆT DIỆT',
        descriptionDTCskillvideo: 'NỘI TẠI',
        descriptionDTCskill2video: 'Theo chu kỳ, đòn đánh kế tiếp của Aatrox gây thêm sát thương vật lý và hồi máu, dựa theo máu tối đa của mục tiêu.',
        DTCskillvideo: '../Videos/VideoNoiTai.mp4'
    },
    skillq: {
        nameDTCskillvideo: 'QUỶ KIẾM DARKIN',
        descriptionDTCskillvideo: 'Q',
        descriptionDTCskill2video: 'Aatrox đập kiếm xuống đất, gây sát thương vật lý. Có thể chém ba lần, mỗi lần có một diện tác dụng khác nhau.',
        DTCskillvideo: '../Videos/VideoSkillQ.mp4'
    },
    skillw: {
        nameDTCskillvideo: 'XIỀNG XÍCH ĐỊA NGỤC',
        descriptionDTCskillvideo: 'W',
        descriptionDTCskill2video: 'Aatrox đập kiếm xuống đất, gây sát thương lên kẻ địch đầu tiên trúng phải. Tướng và quái to phải nhanh rời vùng tác động nếu không muốn bị kéo về tâm và chịu sát thương lần nữa.',
        DTCskillvideo: '../Videos/VideoSkillW.mp4'
    },
    skille: {
        nameDTCskillvideo: 'BỘ PHÁP HẮC ÁM',
        descriptionDTCskillvideo: 'E',
        descriptionDTCskill2video: 'Nội tại giúp Aatrox hồi máu khi gây sát thương lên tướng. Kích hoạt giúp Aatrox lướt theo hướng chỉ định.',
        DTCskillvideo: '../Videos/VideoSkillE.mp4'
    },
    skillr: {
        nameDTCskillvideo: 'CHIẾN BINH TẬN THẾ',
        descriptionDTCskillvideo: 'R',
        descriptionDTCskill2video: 'Aatrox hóa quỷ, làm hoảng sợ lính địch gần đó và được cộng SMCK, tăng hồi máu cũng như Tốc độ Di chuyển. Nếu hắn tham gia hạ gục, hiệu ứng này được kéo dài.',
        DTCskillvideo: '../Videos/VideoSkillR.mp4'
    },
};

function updateDTCSkillContent(DTCVideoSkill) {
    document.querySelector('#mainDTCVideoskillsource').src = DTCVideoSkills[DTCVideoSkill].DTCskillvideo;
    document.querySelector('#mainDTCskillVideo').load();
    document.getElementById('mainNameDTCVideoskill').textContent = DTCVideoSkills[DTCVideoSkill].nameDTCskillvideo;
    document.getElementById('mainDescriptionDTCVdieoskill').textContent = DTCVideoSkills[DTCVideoSkill].descriptionDTCskillvideo;
    document.getElementById('mainDescriptionDTCVdieoskill2').textContent = DTCVideoSkills[DTCVideoSkill].descriptionDTCskill2video;
}

document.addEventListener('DOMContentLoaded', function () {
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dots: false,
        touchDrag: true,
        responsive: {
            320: {
                items: 4
            },
            600: {
                items: 4
            },
            1000: {
                items: 5
            }
        }
    });

    updateDTCSkillContent('noitai');
});
const DTCSkins = {
    skin0: {
        Skinimage: '../Pictures/Skin/Aatrox_0.jpg'
    },
    skin1: {
        Skinimage: '../Pictures/Skin/Aatrox_1.jpg'
    },
    skin2: {
        Skinimage: '../Pictures/Skin/Aatrox_2.jpg'
    },
    skin3: {
        Skinimage: '../Pictures/Skin/Aatrox_3.jpg'
    },
    skin4: {
        Skinimage: '../Pictures/Skin/Aatrox_4.jpg'
    },
    skin5: {
        Skinimage: '../Pictures/Skin/Aatrox_5.jpg'
    },
    skin6: {
        Skinimage: '../Pictures/Skin/Aatrox_6.jpg'
    },
    skin7: {
        Skinimage: '../Pictures/Skin/Aatrox_7.jpg'
    },
    skin8: {
        Skinimage: '../Pictures/Skin/Aatrox_8.jpg'
    },
    skin9: {
        Skinimage: '../Pictures/Skin/Aatrox_5.jpg'
    },
    skin10: {
        Skinimage: '../Pictures/Skin/Aatrox_9.jpg'
    },
    skin11: {
        Skinimage: '../Pictures/Skin/Aatrox_10.jpg'
    },


};
function updateDTCSkinContent(DTCSkin) {
    document.getElementById('mainSkinImage').src = DTCSkins[DTCSkin].Skinimage;
}