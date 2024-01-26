function SetLimit() {
    let count = document.getElementById("countInput").value || 10;

    window.location = "https://localhost:7107/Home/NumbersToN?count=" + count;
}