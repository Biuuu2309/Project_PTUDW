const cancus = {
    nha_ban: {
        cancuname: 'NHÀ CHÍNH CỦA BẠN',
        cancudescription: 'Nhà Chính của bạn là nơi Lính xuất hiện. Phía sau Nhà Chính của bạn là Bệ Đá Cổ, nơi bạn có thể nhanh chóng hồi máu và hồi năng lượng cũng như mở Cửa Hàng.',
        cancuimage: '../Pictures/Backgrounds/trochoi1.png'
    },
    nha_dich: {
        cancuname: 'NHÀ CHÍNH CỦA ĐỊCH',
        cancudescription: 'Nằm sâu trong căn cứ của đội địch, Nhà Chính của địch cũng giống như của bạn. Phá hủy Nhà Chính của địch sẽ giúp đội của bạn giành chiến thắng.',
        cancuimage: '../Pictures/Backgrounds/trochoi4.png'
    },

};


function updateCancuContent(cancu) {
    document.getElementById('mainimageTroChoi').src = cancus[cancu].cancuimage;
    document.getElementById('mainNameTroChoi').textContent = cancus[cancu].cancuname;
    document.getElementById('mainDescriptionTroChoi').textContent = cancus[cancu].cancudescription;
}

const donduongs = {
    tru: {
        donduongname: 'TRỤ',
        donduongdescription: 'Trụ gây sát thương lên lính và tướng địch, đồng thời cung cấp tầm nhìn giới hạn trong Sương Mù cho đồng minh. Hãy cho lính đi vào trước khi tấn công các công trình này để tránh nhận sát thương và xông lên chiến đấu.',
        donduongimage: '../Pictures/Backgrounds/donduong3.png'
    },
    nha_linh: {
        donduongname: 'NHÀ LÍNH',
        donduongdescription: 'Mỗi Nhà Lính đều được bảo vệ bởi 1 Trụ. Khi bị phá hủy, Lính Siêu Cấp sẽ xuất hiện trên đường đó trong vòng vài phút. Sau đó, Nhà Lính sẽ được khôi phục và Lính Siêu Cấp sẽ ngừng xuất hiện.',
        donduongimage: '../Pictures/Backgrounds/donduong4.png'
    },

};


function updateDonduongContent(donduong) {
    document.getElementById('mainimageDonDuong').src = donduongs[donduong].donduongimage;
    document.getElementById('mainNameDonDuong').textContent = donduongs[donduong].donduongname;
    document.getElementById('mainDescriptionDonDuong').textContent = donduongs[donduong].donduongdescription;
}

const jugs = {
    baron: {
        jugname: 'BARON NASHOR',
        jugdescription: 'Baron Nashor là quái vật hùng mạnh nhất trong khu vực rừng. Đội tiêu diệt Baron sẽ được cộng thêm sức mạnh công kích, sức mạnh phép thuật, cường hóa biến về và gia tăng đáng kể sức mạnh của lính xung quanh.',
        jugimage: '../Pictures/Backgrounds/rung3.png'
    },
    rong: {
        jugname: 'RỒNG',
        jugdescription: 'Rồng là những quái vật hùng mạnh với khả năng ban bùa lợi độc nhất tùy theo loại Rồng Nguyên Tố mà đội bạn tiêu diệt. Có 5 Rồng Nguyên Tố và 1 Rồng Ngàn Tuổi.',
        jugimage: '../Pictures/Backgrounds/rung4.png'
    },

};


function updateJugContent(jug) {
    document.getElementById('mainimageJug').src = jugs[jug].jugimage;
    document.getElementById('mainNameJug').textContent = jugs[jug].jugname;
    document.getElementById('mainDescriptionJug').textContent = jugs[jug].jugdescription;
}

const lanes = {
    top: {
        lanename: 'ĐƯỜNG TRÊN',
        lanedescription: 'Các vị tướng đường trên là những đấu sĩ đơn độc ngoan cường của cả đội. Nhiệm vụ của họ là bảo vệ đường của mình và tập trung xử lý các thành viên nguy hiểm nhất của đội địch.',
        laneimage: '../Pictures/Backgrounds/topphai.png'
    },
    rung: {
        lanename: 'RỪNG',
        lanedescription: 'Các vị tướng đi rừng rất đam mê săn mồi. Rình rập giữa các đường một cách lén lút và khéo léo, họ để mắt tới những con quái rừng quan trọng nhất và nhảy ra đoạt mạng khi kẻ địch mất cảnh giác.',
        laneimage: '../Pictures/Backgrounds/rungphai.png'
    },
    mid: {
        lanename: 'ĐƯỜNG GIỮA',
        lanedescription: 'Các vị tướng đường giữa là những nhân tố "chẳng ngán ai" với khả năng dồn sát thương—cho dù là trong chiến đấu đơn lẻ hay giao tranh tổng. Đối với họ, giao tranh là một vũ điệu nguy hiểm, nơi họ luôn tìm kiếm sơ hở của kẻ địch để tiễn chúng lên bảng đếm số.',
        laneimage: '../Pictures/Backgrounds/midphai.png'
    },
    bot: {
        lanename: 'ĐƯỜNG DƯỚI',
        lanedescription: 'Các vị tướng đường dưới giống như những khẩu pháo thủy tinh vậy. Vì rất mong manh, dễ vỡ nên họ cần được bảo vệ trong giai đoạn đầu trận trước khi có thể tích lũy đủ vàng và kinh nghiệm để đưa cả đội tới chiến thắng.',
        laneimage: '../Pictures/Backgrounds/botphai.png'
    },
    sp: {
        lanename: 'HỖ TRỢ',
        lanedescription: 'Các vị tướng hỗ trợ là hộ vệ của cả đội. Họ giúp giữ cho đồng đội sống sót và tập trung vào trợ giúp kiến tạo các pha hạ gục, bảo vệ đồng đội ở đường dưới cho tới khi họ trở nên mạnh mẽ.',
        laneimage: '../Pictures/Backgrounds/spphai.png'
    },

};


function updateLaneContent(lane) {
    document.getElementById('mainimagelane').src = lanes[lane].laneimage;
    document.getElementById('mainNamelane').textContent = lanes[lane].lanename;
    document.getElementById('mainDescriptionlane').textContent = lanes[lane].lanedescription;
}

const powers = {
    exp: {
        namevideopower: 'TÍCH LŨY KINH NGHIỆM',
        descriptionvideopower: 'Khi một tướng thu thập đủ lượng điểm kinh nghiệm nhất định, họ sẽ thăng cấp và có thể mở khóa hoặc cường hóa các kỹ năng cũng như gia tăng chỉ số cơ bản. Thu thập điểm kinh nghiệm bằng cách tiêu diệt/hỗ trợ tiêu diệt lính và tướng địch, cũng như phá hủy các công trình phòng thủ.',
        videopower: '../Videos/Video-19.webm'
    },
    gold: {
        namevideopower: 'KIẾM VÀNG',
        descriptionvideopower: 'Vàng là đơn vị tiền tệ trong trận đấu được sử dụng để mua trang bị cho tướng. Thu thập vàng bằng cách tiêu diệt/hỗ trợ tiêu diệt lính và tướng địch, cũng như phá hủy các công trình phòng thủ và sử dụng những trang bị cộng thêm vàng.',
        videopower: '../Videos/Video-20.webm'
    },
    shop: {
        namevideopower: 'CỬA HÀNG',
        descriptionvideopower: 'Cửa Hàng là nơi bạn có thể mua bán các trang bị bằng số vàng bạn đã kiếm được. Bạn chỉ có thể vào Cửa Hàng khi đứng tại Bệ Đá Cổ.',
        imagepower: '../Pictures/Backgrounds/shop.png'
    },
};

function updatePowerContent(power) {
    const powerData = powers[power];
    document.getElementById('mainNameVideopower').textContent = powerData.namevideopower;
    document.getElementById('mainDescriptionVdieopower').textContent = powerData.descriptionvideopower;

    const videoElement = document.getElementById('mainVideopower');
    const videoSourceElement = document.getElementById('mainVideosourcepower');
    const imageElement = document.getElementById('mainImagepower');

    if (powerData.videopower) {
        videoElement.style.display = 'block';
        imageElement.style.display = 'none';
        videoSourceElement.src = powerData.videopower;
        videoElement.load();
    } else if (powerData.imagepower) {
        videoElement.style.display = 'none';
        imageElement.style.display = 'block';
        imageElement.src = powerData.imagepower;
    }
}

const skillitems = {
    kynang: {
        skillitemname: 'KỸ NĂNG',
        skillitemdescription: 'Đa số các tướng đều sở hữu một bộ kỹ năng độc nhất bao gồm 5 kỹ năng: 1 Nội Tại, 3 Kỹ Năng Cơ Bản và 1 Chiêu Cuối. Những kỹ năng này được gán mặc định cho các phím Q, W, E và R.',
        skillitemimage: '../Pictures/Backgrounds/skillitem4.png'
    },
    spell: {
        skillitemname: 'PHÉP BỔ TRỢ',
        skillitemdescription: 'Phép bổ trợ là những kỹ năng với hiệu ứng độc đáo mà các tướng có thể sử dụng. Chúng được gán mặc định cho phím D và F. Có rất nhiều phép bổ trợ, nhưng các phép được sử dụng phổ biến là Tốc Biến, Dịch Chuyển, Trừng Phạt và Thiêu Đốt.',
        skillitemimage: '../Pictures/Backgrounds/skillitem5.png'
    },
    trangbi: {
        skillitemname: 'TRANG BỊ',
        skillitemdescription: 'Các trang bị sẽ cường hóa sức mạnh cho tướng, ví dụ như tăng tốc độ di chuyển, cải thiện sát thương và giảm thời gian hồi chiêu. Khác với Phụ Kiện, người chơi cần sử dụng vàng để mua trang bị.',
        skillitemimage: '../Pictures/Backgrounds/skillitem6.png'
    },
};

function updateSkillitemContent(skillitem) {
    document.getElementById('mainimageskillitem').src = skillitems[skillitem].skillitemimage;
    document.getElementById('mainNameskillitem').textContent = skillitems[skillitem].skillitemname;
    document.getElementById('mainDescriptionskillitem').textContent = skillitems[skillitem].skillitemdescription;
}