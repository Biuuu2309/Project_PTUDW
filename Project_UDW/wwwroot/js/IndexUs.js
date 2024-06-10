const modes = {
    vodai: {
        namevideo: 'HỠI CÁC CẶP BÀI TRÙNG',
        descriptionvideo: 'Hãy vượt qua hàng loạt bản đồ, phối hợp các nâng cấp và trang bị tuyệt hảo để cùng người anh em chí cốt của bạn leo hạng trong giải đấu 2v2v2v2v2v2v2v2 này nhé.',
        video: '../Videos/Video-15.mp4'
    },
    summoner: {
        namevideo: 'CHẾ ĐỘ CHƠI PHỔ BIẾN NHẤT',
        descriptionvideo: 'Dọn đường, tham gia giao tranh tổng, phá hủy Nhà Chính của địch trước khi Nhà Chính của bạn bị phá hủy.',
        video: '../Videos/Video-16.mp4'
    },
    aram: {
        namevideo: 'TẤT CẢ NGẪU NHIÊN, CHỈ ĐI ĐƯỜNG GIỮA',
        descriptionvideo: 'Chiến đấu trên một cây cầu băng giá với những vị tướng ngẫu nhiên để xông thẳng tới Nhà Chính của địch trong chế độ 5v5 vui nhộn và hỗn loạn.',
        video: '../Videos/Video-17.mp4'
    },
    tft: {
        namevideo: 'MỘT TRẬN HỖN CHIẾN ĐỂ GIÀNH NGÔI VỊ BÁ CHỦ',
        descriptionvideo: 'Tập hợp những vị tướng để chiến đấu cho bạn. Vượt qua 7 đối thủ khác để trở thành người sống sót cuối cùng.',
        video: '../Videos/Video-18.mp4'
    },
};

function updateModeContent(mode) {
    document.querySelector('#mainVideosource').src = modes[mode].video;
    document.querySelector('#mainVideo').load();
    document.getElementById('mainNameVideo').textContent = modes[mode].namevideo;
    document.getElementById('mainDescriptionVdieo').textContent = modes[mode].descriptionvideo;
}
const champions = {
    sat_thu: {
        name: 'AKALI',
        description: 'Sát Thủ Đơn Độc',
        image: '../Pictures/Champions Update/P12.png'
    },
    dau_si: {
        name: 'YASUO',
        description: 'Kẻ Bất Dung Thứ',
        image: '../Pictures/Champions Update/P13.png'
    },
    phap_su: {
        name: 'LUX',
        description: 'Tiểu Thư Ánh Sáng',
        image: '../Pictures/Champions Update/P14.png'
    },
    xa_thu: {
        name: 'JINX',
        description: 'Khẩu Pháo Nổi Loạn',
        image: '../Pictures/Champions Update/P15.png'
    },
    ho_tro: {
        name: 'THRESH',
        description: 'Cai Ngục Xiềng Xích',
        image: '../Pictures/Champions Update/P16.png'
    },
    do_don: {
        name: 'LEONA',
        description: 'Bình Minh Rực Rỡ',
        image: '../Pictures/Champions Update/P17.png'
    },

};


function updateChampionContent(champion) {
    document.getElementById('mainImage').src = champions[champion].image;
    document.getElementById('mainName').textContent = champions[champion].name;
    document.getElementById('mainDescription').textContent = champions[champion].description;
}