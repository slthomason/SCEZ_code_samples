var base_url = 'https://api-test.smartcontractsez.com/api/v1/'; //For mainnet, use https://api.smartcontractsez.com/api/v1/
var api_key = '21140209e56752bfcdd10e5c34ce74e8';

const handleImageUpload = event => {
    const files = event.target.files

    console.log(files[0]);

}

document.querySelector('#formFile').addEventListener('change', event => {
    handleImageUpload(event)
})

var default_wallet_id = 0;

var that = this;
getWalletBalance().then(data => {

    that.default_wallet_id = data['default_wallet_id'];
    console.log(that.default_wallet_id);
    if (that.default_wallet_id <= 0)
        document.getElementById('txtWalletAddress').style.display = "block";

});



function minNft() {


    try {
        console.log('clicked form');
        var networkType = document.getElementById('ddlNetType').value;
        //Use mainnet api key
        if (networkType == 'mainnet') {
            base_url = 'https://api.smartcontractsez.com/api/v1/';
            api_key = '';
        }

        var tokenName = document.getElementById('txtTokenName').value;
        var description = document.getElementById('txtDescription').value;
        var input = document.getElementById('formFile');
        var file = input.files[0];

        console.log(file);
        if (tokenName == '' || description == '' || input.files.length == 0) {
            alert('Required fields missing!');
            return;

        }

        //Ensure detault wallet address is added where minted NFT will be sent to if not exists already

        if (that.default_wallet_id <= 0) {

            var walletName = document.getElementById('txtWalletAddress').value;

            if (walletName == '') {
                alert('Add valid ADA address');
                return;

            }

            var jsonData = {
                wallet_address: walletName,
                wallet_name: "default"
            }

            createWallet(jsonData).then(data => {
                console.log(data);

            });

        }

        //Step #1: Get default wallet
        getWallets().then(data => {

            console.log(data);
            if (data?.length > 0) {

                var default_wallet = data[0];

                //Step #2: Create nft
                var jsonData = {
                    token_name: tokenName,
                    description: description,
                    wallet_id: default_wallet['id']
                }

                createNft(jsonData).then(data => {
                    console.log(data);
                    //Step #3: upload image
                    if (data != null && data != undefined) {

                        var order_id = data['order_id'];
                        uploadNftImage(order_id, file).then(data => {
                            alert('Nft minted successfully');
                            window.location.reload();
                        });
                    }
                });
            }
        });

    } catch (error) {
        console.error(error);
    }
}

//Get wallet balance
async function getWalletBalance() {

    try {
        var url = base_url + 'wallet/balance';
        const responseData = await getData(url, api_key);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}

//Get wallets
async function getWallets() {

    try {
        var url = base_url + 'wallet/list';
        const responseData = await getData(url, api_key);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}

//Create Wallet
async function createWallet(data) {

    try {
        var url = base_url + 'wallet';
        const responseData = await postData(url, api_key, data);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}

//Create Nft
async function createNft(data) {

    try {
        var url = base_url + 'nft';
        const responseData = await postData(url, api_key, data);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}

//UploadNftImage
async function uploadNftImage(order_id, file) {

    try {
        var url = base_url + 'nft/asset/upload?order_id=' + order_id;
        const responseData = await postForm(url, api_key, file);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}


//Get NFT Orders
async function getNftOrders() {

    try {
        var url = base_url + 'nft/list';
        const responseData = await getData(url, api_key);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}



//Get Transactions
async function getTransactions() {

    try {
        var url = base_url + 'user/transactions/list';
        const responseData = await getData(url, api_key);
        console.log({ responseData });
        return responseData;
    } catch (error) {
        console.error(error);
    }
}


