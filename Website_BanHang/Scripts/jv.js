function register(e){
    var username=document.getElementById("username").value;
    var email=document.getElementById("email").value;
    var password=document.getElementById("password").value;
    var confirmpassword=document.getElementById("confirmpassword").value;
    if(password != confirmpassword){
        alert("Vui lòng nhập đúng mật khẩu !");
        return;
    }else{
        alert("Đăng kí thành công !");
    }
    const user={
        username:username,
        email:email,
        password:password,
        confirmpassword:confirmpassword,
    }
    var json=JSON.stringify(user);
    localStorage.setItem(email,json);
    
    window.location.href="Login.html";
    return false;
}

function login(e){
    var email=document.getElementById("email").value;
    var password=document.getElementById("password").value;
    var user=localStorage.getItem(email);
    var data=JSON.parse(user);
    var username=data.username;
    
    if(email==data.email && password==data.password){
        alert("đăng nhập thành công !");
        window.location.href="index.html";
        //Ngăn form được gửi đi
        //Ngăn người dùng đăng nhập với thông tin không chính xác
        return false;   
    }else if(email==data.email && password!=data.password){
        alert("Mật khẩu không chính xác !");
    }else {
        alert("Tài khoản không tồn tại !");
    }
    //Lấy tên người dùng để hiển thị lên index
    document.getElementById("Userlogin").innerText = "Chào mừng, " + username + "!";
}