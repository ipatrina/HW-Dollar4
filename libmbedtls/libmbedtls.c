#pragma comment(lib, "bcrypt.lib")

#include <windows.h>
#include "mbedtls/md.h"
#include "mbedtls/pkcs5.h"
#include "mbedtls/gcm.h"

#ifdef _WIN32
#define DLL_EXPORT __declspec(dllexport)
#else
#define DLL_EXPORT
#endif

#define IV_LEN 16
#define TAG_LEN 16
#define KEY_LEN 32
#define KEY_BITS 256

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpReserved) {
    switch (fdwReason) {
    case DLL_PROCESS_ATTACH:
        break;
    case DLL_PROCESS_DETACH:
        break;
    case DLL_THREAD_ATTACH:
        break;
    case DLL_THREAD_DETACH:
        break;
    }
    return TRUE;
}

DLL_EXPORT int libmbedtls_test_add(int a, int b) {
    return a + b;
}

DLL_EXPORT int libmbedtls_test_subtract(int a, int b) {
    return a - b;
}

DLL_EXPORT int libmbedtls_md_hmac_sha256(unsigned char* key, unsigned int key_len, unsigned char* data, unsigned int data_len, unsigned char* hmac_result) {
    mbedtls_md_context_t ctx;
    const mbedtls_md_info_t* md_info = mbedtls_md_info_from_type(MBEDTLS_MD_SHA256);

    mbedtls_md_init(&ctx);
    mbedtls_md_setup(&ctx, md_info, 1);
    mbedtls_md_hmac_starts(&ctx, key, key_len);
    mbedtls_md_hmac_update(&ctx, data, data_len);
    mbedtls_md_hmac_finish(&ctx, hmac_result);
    mbedtls_md_free(&ctx);

    return 0;
}

DLL_EXPORT int libmbedtls_gcm_crypt_and_tag_pbkdf2(unsigned char* password, unsigned int password_len, unsigned char* salt, unsigned int salt_len, unsigned int data_len, unsigned char* iv, unsigned char* input_data, unsigned char* cipher_data, unsigned char* tag) {
    unsigned char key[KEY_LEN];
    mbedtls_md_context_t md_ctx;
    mbedtls_md_init(&md_ctx);
    const mbedtls_md_info_t* md_info = mbedtls_md_info_from_type(MBEDTLS_MD_SHA256);
    mbedtls_md_setup(&md_ctx, md_info, 1);
    mbedtls_pkcs5_pbkdf2_hmac(&md_ctx, password, password_len, salt, salt_len, 1, KEY_LEN, key);
    mbedtls_md_free(&md_ctx);

    mbedtls_gcm_context gcm;
    mbedtls_gcm_init(&gcm);
    mbedtls_gcm_setkey(&gcm, MBEDTLS_CIPHER_ID_AES, key, KEY_BITS);
    int ret = mbedtls_gcm_crypt_and_tag(&gcm, MBEDTLS_GCM_ENCRYPT, data_len, iv, IV_LEN, NULL, 0, input_data, cipher_data, TAG_LEN, tag);
    mbedtls_gcm_free(&gcm);

    return ret;
}

DLL_EXPORT int libmbedtls_gcm_auth_decrypt_pbkdf2(unsigned char* password, unsigned int password_len, unsigned char* salt, unsigned int salt_len, unsigned char* iv, unsigned char* tag, unsigned char* cipher_data, unsigned int data_len, unsigned char* decrypt_data) {
    unsigned char key[KEY_LEN];
    mbedtls_md_context_t md_ctx;
    mbedtls_md_init(&md_ctx);
    const mbedtls_md_info_t* md_info = mbedtls_md_info_from_type(MBEDTLS_MD_SHA256);
    mbedtls_md_setup(&md_ctx, md_info, 1);
    mbedtls_pkcs5_pbkdf2_hmac(&md_ctx, password, password_len, salt, salt_len, 1, KEY_LEN, key);
    mbedtls_md_free(&md_ctx);

    mbedtls_gcm_context gcm;
    mbedtls_gcm_init(&gcm);
    mbedtls_gcm_setkey(&gcm, MBEDTLS_CIPHER_ID_AES, key, KEY_BITS);
    int ret = mbedtls_gcm_auth_decrypt(&gcm, data_len, iv, IV_LEN, NULL, 0, tag, TAG_LEN, cipher_data, decrypt_data);
    mbedtls_gcm_free(&gcm);
    
    return ret;
}