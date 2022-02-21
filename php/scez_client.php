<?php

class ScezClient
{


    var $apiKey = "25d55ad283aa400af464c76d713c07ad"; //Get from dashboard
    var $baseUrl = "https://api-test.smartcontractsez.com/api/v2/"; //For mainnet, use https://api.smartcontractsez.com/api/v2/



    //Mint nft

    function mintNft($tokenName, $tokenDescription, $filePath)
    {

        //Step #1: Create NFT
        $order =  json_decode($this->createNft($tokenName, $tokenDescription));
        if ($order != null) {
            //Step #2: Upload NFT Image
            $nftImage =  $this->uploadNftImage($order->OrderID, $filePath);

            echo $nftImage;
        }
           
       
    }



    ///Get my-wallet
    function getMyWalletData()
    {
        $curl = curl_init();

        curl_setopt_array($curl, [
            CURLOPT_SSL_VERIFYPEER => false,
            CURLOPT_URL => $this->baseUrl . "wallet/my-wallet",
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_ENCODING => "",
            CURLOPT_TIMEOUT => 60,
            CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
            CURLOPT_CUSTOMREQUEST => "GET",
            CURLOPT_HTTPHEADER => [
                "x-api-key: $this->apiKey",
                "Content-Type: application/json"
            ],
        ]);

        $response = curl_exec($curl);
        $err = curl_error($curl);

        curl_close($curl);

        if ($err) {
            echo "cURL Error #:" . $err;
        } else {
            return $response;
        }
    }


    //Create nft
    function createNft($tokenName, $tokenDescription)
    {
        $curl = curl_init();

        curl_setopt_array($curl, [
            CURLOPT_SSL_VERIFYPEER => false,
            CURLOPT_URL => $this->baseUrl . "nft",
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_ENCODING => "",
            CURLOPT_TIMEOUT => 60,
            CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
            CURLOPT_CUSTOMREQUEST => "POST",
            CURLOPT_POSTFIELDS => "{\n  \"token_name\": \"$tokenName\",\n  \"description\": \"$tokenDescription\",\n}",
            CURLOPT_HTTPHEADER => [
                "x-api-key: $this->apiKey",
                "Content-Type: application/json"
            ],
        ]);

        $response = curl_exec($curl);
        $err = curl_error($curl);

        curl_close($curl);
        var_dump($response);
        if ($err) {
            echo "cURL Error #:" . $err;
        } else {
            return $response;
        }
    }


    //Upload Nft image
    function uploadNftImage($orderID, $filePath)
    {
       

        $post_data = array(
            'file' => $filePath
        );


        $curl = curl_init();

        curl_setopt_array($curl, [
            CURLOPT_SSL_VERIFYPEER => false,
            CURLOPT_URL => $this->baseUrl . "nft/asset/upload?order_id=$orderID",
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_ENCODING => "",
            CURLOPT_TIMEOUT => 60,
            CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
            CURLOPT_CUSTOMREQUEST => "POST",
            CURLOPT_POSTFIELDS, $post_data,
            CURLOPT_HTTPHEADER => [
                "x-api-key: $this->apiKey",
                "Content-Type: multipart/form-data"
            ],
        ]);

        $response = curl_exec($curl);
        $err = curl_error($curl);

        curl_close($curl);

        if ($err) {
            echo "cURL Error #:" . $err;
        } else {
            return $response;
        }
    }

    function build_data_files($boundary, $fields, $files)
    {
        $data = '';
        $eol = "\r\n";

        $delimiter = '-------------' . $boundary;

        foreach ($fields as $name => $content) {
            $data .= "--" . $delimiter . $eol
                . 'Content-Disposition: form-data; name="' . $name . "\"" . $eol . $eol
                . $content . $eol;
        }


        foreach ($files as $name => $content) {
            $data .= "--" . $delimiter . $eol
                . 'Content-Disposition: form-data; name="' . $name . '"; filename="' . $name . '"' . $eol
                //. 'Content-Type: image/png'.$eol
                . 'Content-Transfer-Encoding: binary' . $eol;

            $data .= $eol;
            $data .= $content . $eol;
        }
        $data .= "--" . $delimiter . "--" . $eol;


        return $data;
    }
}
