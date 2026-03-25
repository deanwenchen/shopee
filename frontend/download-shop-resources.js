import fs from 'fs'
import path from 'path'
import https from 'https'
import { fileURLToPath } from 'url'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// 从 Figma 设计上下文提取的所有资源 URL
const resources = {
  // === 认证页面资源（旧 UUID 映射到新描述性名称）===
  // Login Page - Bubbles
  'login-bubble-02': 'https://www.figma.com/api/mcp/asset/56fc5330-5d3d-4979-a050-37d85c359820',
  'login-bubble-01': 'https://www.figma.com/api/mcp/asset/a007d904-91e7-4dab-92c2-975266a4496a',
  'login-bubble-03': 'https://www.figma.com/api/mcp/asset/4047a2b8-7488-4f38-a0d0-fd665eeb8276',
  'login-bubble-04': 'https://www.figma.com/api/mcp/asset/a8768779-5a96-411c-83de-55b550d454d1',
  'login-emoticon': 'https://www.figma.com/api/mcp/asset/0e7c1044-1b0e-4025-ba9a-fd788bf676d4',

  // Password Page - Bubbles & Avatar
  'password-bubble-02': 'https://www.figma.com/api/mcp/asset/090d6144-e1da-4b62-8d46-af67a8b62405',
  'password-bubble-01': 'https://www.figma.com/api/mcp/asset/9acc6ed4-83e1-4c99-b930-5279f1abf2b7',
  'password-avatar-frame': 'https://www.figma.com/api/mcp/asset/55897da1-4abd-446d-9448-8ea2bb86b45d',

  // PasswordRecovery Page - Bubbles & Avatar
  'recovery-bubble-01': 'https://www.figma.com/api/mcp/asset/1afb7a0d-c11b-4a9a-8e61-5ead6fffec23',
  'recovery-bubble-02': 'https://www.figma.com/api/mcp/asset/d31e2712-7080-4907-9c35-e4baff6bdee5',
  'recovery-ellipse': 'https://www.figma.com/api/mcp/asset/05b73873-ad02-4a83-bbc3-947026b13eee',
  'recovery-avatar-deco': 'https://www.figma.com/api/mcp/asset/aa0e35dd-f8fd-4072-b35f-834ff4a95a73',
  'recovery-avatar-photo': 'https://www.figma.com/api/mcp/asset/0c20e9cb-8161-4915-9fa3-6837f4bebe14',
  'recovery-avatar-mask': 'https://www.figma.com/api/mcp/asset/6cd29425-f045-434b-9bba-edf0f4f699df',
  'recovery-avatar-main': 'https://www.figma.com/api/mcp/asset/877a4afd-bfed-49c8-90dc-5b7809c99b2e',
  'recovery-check-icon': 'https://www.figma.com/api/mcp/asset/8dfe51c7-d0b1-4bb8-b502-ed108389b3f5',
  'recovery-check-empty': 'https://www.figma.com/api/mcp/asset/ad430095-ecd8-42b2-841c-8572415af049',

  // PasswordRecoveryCode Page - Bubbles & Avatar
  'code-bubble-01': 'https://www.figma.com/api/mcp/asset/4f3439f4-62ab-4010-a149-fa9a7d441c99',
  'code-bubble-02': 'https://www.figma.com/api/mcp/asset/0a65a368-1b3f-46f0-ac21-a12833a0e3e7',
  'code-ellipse': 'https://www.figma.com/api/mcp/asset/b6068eeb-70af-498c-9980-3dffc3091251',
  'code-avatar-deco': 'https://www.figma.com/api/mcp/asset/b4b25371-b629-4555-975e-26e544aaa638',
  'code-avatar-photo': 'https://www.figma.com/api/mcp/asset/332d8b03-bf20-4595-9f49-76a62e79acf3',
  'code-avatar-mask': 'https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322',
  'code-avatar-main': 'https://www.figma.com/api/mcp/asset/c0f59a89-b825-4bcc-8849-8df1cc7f7e9d',
  'code-warning-icon': 'https://www.figma.com/api/mcp/asset/cf9766c4-dba1-439a-9506-9618ca76549a',
  'code-dot-empty': 'https://www.figma.com/api/mcp/asset/83ba2ab2-92b5-4d1e-9c94-e321540274a4',

  // NewPassword Page - Bubbles & Avatar
  'newpass-bubble-01': 'https://www.figma.com/api/mcp/asset/f04821bb-dcc4-46ca-b2a1-5b59f443bf5f',
  'newpass-ellipse': 'https://www.figma.com/api/mcp/asset/58006e4b-da06-4d80-a4a0-b32cbb465954',
  'newpass-avatar-deco': 'https://www.figma.com/api/mcp/asset/77e59ace-f77e-4a18-b27f-b1ae8617b340',
  'newpass-avatar-photo': 'https://www.figma.com/api/mcp/asset/a86862a8-6a33-4c94-9b81-952d3960ae81',
  'newpass-avatar-main': 'https://www.figma.com/api/mcp/asset/159d3385-63e5-47de-b7ec-10f295efffbe',

  // HelloCard Page - Dots & Bubbles
  'hello-dot-filled': 'https://www.figma.com/api/mcp/asset/f71bedda-e6dc-4e2e-b7cc-b5a3d8d7a968',
  'hello-dot-empty': 'https://www.figma.com/api/mcp/asset/594e48ee-e099-46ec-a7a9-1faacfc838ab',
  'hello-bubble-02': 'https://www.figma.com/api/mcp/asset/f18f3704-8a74-4131-9676-001a3c95bb7f',
  'hello-bubble-01': 'https://www.figma.com/api/mcp/asset/13d94a9f-7c9e-4eb3-9e97-a51f4dc1e843',

  // CreateAccount Page - Bubbles & Icons
  'signup-bubbles': 'https://www.figma.com/api/mcp/asset/70ae7a39-be04-4867-96d7-b393726d02ac',
  'signup-upload-frame': 'https://www.figma.com/api/mcp/asset/5cf44006-dae9-4be9-ac93-3937d16a9be3',
  'signup-camera-icon': 'https://www.figma.com/api/mcp/asset/df477442-f952-447f-9e34-26b0260de677',
  'signup-flag': 'https://www.figma.com/api/mcp/asset/d85b2ef1-423c-43fb-9a18-791e4f7189a5',
  'signup-dropdown': 'https://www.figma.com/api/mcp/asset/ecb20b03-74c8-439a-a583-1c3142b33d8e',

  // === Shop 页面资源 ===
  // Status Bar 资源
  'bars-status-bar': 'https://www.figma.com/api/mcp/asset/8b2549d7-6bca-4236-ab57-de84371c0935',
  'battery-border': 'https://www.figma.com/api/mcp/asset/9e9b75f5-5cb0-42b6-b131-ccd376ded58a',
  'battery-cap': 'https://www.figma.com/api/mcp/asset/d02eacd9-2bbf-4fa8-ac45-55a8a9a85422',
  'battery-capacity': 'https://www.figma.com/api/mcp/asset/e7a676ae-e0c0-442f-a3bd-be3573ced975',
  'wifi': 'https://www.figma.com/api/mcp/asset/954f8a97-fe8b-40b4-acf8-7fb2a9cf2197',
  'cellular': 'https://www.figma.com/api/mcp/asset/58c0c28d-33eb-4eb5-9860-668377d2ca58',
  'time-background': 'https://www.figma.com/api/mcp/asset/b30424ed-ac6b-4836-a25c-6e9c2ac6da05',

  // Search 资源
  'search-icon': 'https://www.figma.com/api/mcp/asset/0283ea2e-28ab-41b0-bed6-f0325c86604b',
  'search-path': 'https://www.figma.com/api/mcp/asset/66c36399-3da1-4652-b502-d81dbe16c3a0',

  // Big Sale Banner 资源
  'bubble-01': 'https://www.figma.com/api/mcp/asset/a1ec2494-43b7-48ca-a759-ee783368513a',
  'bubble-02': 'https://www.figma.com/api/mcp/asset/f8fd884e-6db6-474a-bb99-10cd3db0cfbf',
  'bubble-02-alt': 'https://www.figma.com/api/mcp/asset/67f01fd7-7558-425a-8517-bf0bf8490287',
  'bubble-03': 'https://www.figma.com/api/mcp/asset/8819350a-0ea5-411c-b38a-b56f1f883cdb',
  'banner-controls': 'https://www.figma.com/api/mcp/asset/e10d2f63-5c55-4c43-b87a-7aa19fcf54a4',

  // Categories 资源 - Clothing
  'cat-clothing-main': 'https://www.figma.com/api/mcp/asset/7c3a85aa-4912-4c24-bb62-bd7900df536a',
  'cat-clothing-1': 'https://www.figma.com/api/mcp/asset/c11a99e1-7a5d-4db7-a136-910e70dd3092',
  'cat-clothing-2': 'https://www.figma.com/api/mcp/asset/b219cc9a-9117-4eb8-8cbc-894b9af8ad3d',
  'cat-clothing-3': 'https://www.figma.com/api/mcp/asset/96d27e13-8e49-4f8d-a151-1f5f2061cd68',

  // Categories 资源 - Bags
  'cat-bags-main': 'https://www.figma.com/api/mcp/asset/b542db70-6aaf-4173-b311-32c5b9d01146',
  'cat-bags-1': 'https://www.figma.com/api/mcp/asset/c524bf55-f2b1-4d87-b8c8-b9251d5161c8',
  'cat-bags-2': 'https://www.figma.com/api/mcp/asset/1965bad8-cf38-4818-b456-33a493ccb418',
  'cat-bags-3': 'https://www.figma.com/api/mcp/asset/36e80de1-e017-4de6-a831-c151c38e797e',

  // Categories 资源 - Shoes
  'cat-shoes-main': 'https://www.figma.com/api/mcp/asset/dd687db0-11bd-47e4-9cf7-2eeb6ae606f1',
  'cat-shoes-1': 'https://www.figma.com/api/mcp/asset/147f578d-a431-4c2c-8352-1d6a0806e3b6',
  'cat-shoes-2': 'https://www.figma.com/api/mcp/asset/265d3eb3-07dd-4fc6-8b23-56d8a5fa9a32',
  'cat-shoes-3': 'https://www.figma.com/api/mcp/asset/cf4ce572-c23e-4d27-95ef-36570be61c1e',

  // Categories 资源 - Lingerie
  'cat-lingerie-main': 'https://www.figma.com/api/mcp/asset/cf4ce572-c23e-4d27-95ef-36570be61c1e',
  'cat-lingerie-1': 'https://www.figma.com/api/mcp/asset/d4194603-e686-4998-9bc5-72eb8cbf0a29',
  'cat-lingerie-2': 'https://www.figma.com/api/mcp/asset/8358e331-daad-4766-9607-dc4e9e055198',
  'cat-lingerie-3': 'https://www.figma.com/api/mcp/asset/35e4316e-159e-49fc-85de-fbbc20efeb0d',

  // Categories 资源 - Watch
  'cat-watch-1': 'https://www.figma.com/api/mcp/asset/fec560ca-2003-4f32-ab94-14c240302590',
  'cat-watch-2': 'https://www.figma.com/api/mcp/asset/55678a85-6f77-4c24-8269-f4d6a82e84a9',

  // Categories 资源 - Hoodies
  'cat-hoodies-1': 'https://www.figma.com/api/mcp/asset/6ff4bd5a-9242-47d1-990d-988d06f58b94',
  'cat-hoodies-2': 'https://www.figma.com/api/mcp/asset/178776f1-047f-4ce5-a156-13917e3261bd',

  // Categories 资源 - Dresses
  'cat-dresses-1': 'https://www.figma.com/api/mcp/asset/229cdfdc-cd8f-4403-9333-884492de83e9',
  'cat-dresses-2': 'https://www.figma.com/api/mcp/asset/6c7dd4d4-c400-49ec-9958-987c1b1df159',

  // Top Products 资源
  'top-products-ellipse': 'https://www.figma.com/api/mcp/asset/0bebf661-f8a6-48ca-a759-da94af18722f',
  'top-dresses': 'https://www.figma.com/api/mcp/asset/9129fe5f-0094-4b92-b0d4-9767764d6952',
  'top-tshirts': 'https://www.figma.com/api/mcp/asset/ab510fb1-4569-41f8-a0a6-bf80cbbd43e1',
  'top-skirts': 'https://www.figma.com/api/mcp/asset/4b6bc45c-9af0-4da3-95e0-757c741e4338',
  'top-shoes': 'https://www.figma.com/api/mcp/asset/a4c43577-c297-44be-868d-0948b20150e2',
  'top-bags': 'https://www.figma.com/api/mcp/asset/3c21120c-e090-4d2d-a370-8af33f550cb4',

  // New Items 资源
  'new-item-1': 'https://www.figma.com/api/mcp/asset/1b5f53b1-a1b4-47c5-92f8-c30417bda67d',
  'new-item-2': 'https://www.figma.com/api/mcp/asset/bd70d2ea-3e3a-471d-8343-335a49cfa9d7',
  'new-item-3': 'https://www.figma.com/api/mcp/asset/5ad0289e-9df2-4078-b4b0-a29a4919a3e9',
  'new-item-mask': 'https://www.figma.com/api/mcp/asset/4adad59c-55d4-4d51-985a-e66c7191b6cb',
  'new-item-mask-2': 'https://www.figma.com/api/mcp/asset/70403b3b-1192-4c41-9adf-727c0a9e78c2',

  // Flash Sale 资源
  'flash-1': 'https://www.figma.com/api/mcp/asset/0386803d-c673-459c-aa2b-bad5d8bce0c4',
  'flash-2': 'https://www.figma.com/api/mcp/asset/1cf0a5d7-6195-4429-a905-95a6c69de5f3',
  'flash-3': 'https://www.figma.com/api/mcp/asset/f2421ebb-1426-4947-9f16-9a5d8ae4d8b0',
  'flash-4': 'https://www.figma.com/api/mcp/asset/dd311c4b-de3b-44fc-a220-974135ff54d5',
  'flash-5': 'https://www.figma.com/api/mcp/asset/e5ad90eb-22ec-49ab-ab60-8041a2df9292',
  'flash-6': 'https://www.figma.com/api/mcp/asset/1034ef28-c62c-4b54-960a-11a9dfdd9312',
  'flash-discount-bg': 'https://www.figma.com/api/mcp/asset/d04b55c4-a3ad-48d2-9a37-b0092eb6e31a',
  'flash-clock': 'https://www.figma.com/api/mcp/asset/ed1392df-9f9e-411a-aa77-775c3daafa24',

  // Most Popular 资源
  'popular-1': 'https://www.figma.com/api/mcp/asset/b51b80fa-96c7-49dc-bf03-4c8f446f345d',
  'popular-2': 'https://www.figma.com/api/mcp/asset/cf4ce572-c23e-4d27-95ef-36570be61c1e',
  'popular-3': 'https://www.figma.com/api/mcp/asset/d04b55c4-a3ad-48d2-9a37-b0092eb6e31a',
  'popular-4': 'https://www.figma.com/api/mcp/asset/e5ad90eb-22ec-49ab-ab60-8041a2df9292',
  'popular-like': 'https://www.figma.com/api/mcp/asset/eeac10c5-bd6b-47ec-a2fd-4d2b98e5712d',
  'popular-button': 'https://www.figma.com/api/mcp/asset/f47c6c8e-ed29-4bbd-ac4e-ff6aeef65b44',
  'popular-arrow': 'https://www.figma.com/api/mcp/asset/0ee6ad3b-d542-4aa3-a441-aa4d5c9a1f8e',
  'popular-star': 'https://www.figma.com/api/mcp/asset/cf06a42a-1e41-4ef5-b71c-429b769d2783',

  // Just For You 资源
  'jfy-1': 'https://www.figma.com/api/mcp/asset/ef4102e4-854e-44a6-ae08-21c9008c5143',
  'jfy-2': 'https://www.figma.com/api/mcp/asset/84a836dc-5869-4466-a478-4ba6bac9a3e8',
  'jfy-3': 'https://www.figma.com/api/mcp/asset/1034ef28-c62c-4b54-960a-11a9dfdd9312',
  'jfy-4': 'https://www.figma.com/api/mcp/asset/f2421ebb-1426-4947-9f16-9a5d8ae4d8b0',

  // Bottom Navigation 资源
  'nav-shopping-bag': 'https://www.figma.com/api/mcp/asset/cf8e473e-ec5d-4e3b-b329-51281332f801',
  'nav-path-335': 'https://www.figma.com/api/mcp/asset/cfddd62d-3916-4905-a902-70a18b9c6526',
  'nav-path-336': 'https://www.figma.com/api/mcp/asset/0ac1c7af-27bb-4a14-972e-513cec55d9ae',
  'nav-mark-01': 'https://www.figma.com/api/mcp/asset/befa1980-0373-486a-aa7c-cef1ffba28d4',
  'nav-heart': 'https://www.figma.com/api/mcp/asset/6c971d36-cd1a-453d-acaa-dced626ae27b',
  'nav-rectangle': 'https://www.figma.com/api/mcp/asset/4891b315-efcd-4357-9a75-f2dd1f784d68',
  'nav-menu': 'https://www.figma.com/api/mcp/asset/a17b76f3-bd89-414d-8fc6-46c2485720da',
  'nav-home': 'https://www.figma.com/api/mcp/asset/c183b04b-416c-470e-a64f-755c44a5315c',
  'nav-path-328': 'https://www.figma.com/api/mcp/asset/741b9f67-59e1-44f0-8088-2f4f0d1ccbc8',
  'nav-mark-2': 'https://www.figma.com/api/mcp/asset/fe8ac2f4-16e6-487f-b876-27fc1d2c5fe4',
  'nav-profile': 'https://www.figma.com/api/mcp/asset/7f349ce3-52ae-4961-a7bc-9eee18aecf48',
}

const outputDir = path.join(__dirname, 'src', 'assets', 'figma')

// 确保输出目录存在
if (!fs.existsSync(outputDir)) {
  fs.mkdirSync(outputDir, { recursive: true })
}

// 检测文件类型并返回正确的扩展名
function getFileExtension(buffer) {
  // 检查 PNG 魔数 (前 8 个字节)
  if (buffer[0] === 0x89 && buffer[1] === 0x50 && buffer[2] === 0x4E && buffer[3] === 0x47) {
    return 'png'
  }
  // 检查 JPEG 魔数
  if (buffer[0] === 0xFF && buffer[1] === 0xD8 && buffer[2] === 0xFF) {
    return 'jpg'
  }
  // 检查 GIF 魔数
  if (buffer[0] === 0x47 && buffer[1] === 0x49 && buffer[2] === 0x46) {
    return 'gif'
  }
  // 检查 SVG (文本文件，检查是否包含 <svg)
  const header = buffer.toString('utf8', 0, Math.min(100, buffer.length))
  if (header.includes('<svg') || header.includes('<?xml')) {
    return 'svg'
  }
  // 默认返回 png
  return 'png'
}

// 下载文件的函数
function downloadFile(url, filename) {
  return new Promise((resolve, reject) => {
    console.log(`Downloading: ${filename}`)

    https.get(url, (response) => {
      if (response.statusCode !== 200) {
        reject(new Error(`Failed to download ${filename}: ${response.statusCode}`))
        return
      }

      const chunks = []

      response.on('data', (chunk) => {
        chunks.push(chunk)
      })

      response.on('end', () => {
        const buffer = Buffer.concat(chunks)

        // 检测实际文件类型
        const actualExt = getFileExtension(buffer)

        // 如果原文件名扩展名与实际内容不匹配，则修正
        const baseName = filename.replace(/\.(svg|png|jpg|gif)$/, '')
        const finalFilename = `${baseName}.${actualExt}`

        const filePath = path.join(outputDir, finalFilename)

        fs.writeFile(filePath, buffer, (err) => {
          if (err) {
            reject(err)
            return
          }

          if (actualExt !== 'svg') {
            console.log(`⚠ Saved: ${finalFilename} (detected: ${actualExt.toUpperCase()})`)
          } else {
            console.log(`✓ Saved: ${finalFilename}`)
          }
          resolve(finalFilename)
        })
      })
    }).on('error', (err) => {
      fs.unlink(path.join(outputDir, filename), () => {})
      reject(err)
    })
  })
}

// 主函数
async function main() {
  console.log('Starting to download Figma resources...\n')

  const entries = Object.entries(resources)
  let success = 0
  let failed = 0
  const savedFiles = []

  for (const [name, url] of entries) {
    try {
      // 根据 URL 生成文件名
      const urlParts = url.split('/')
      const assetId = urlParts[urlParts.length - 1]
      const filename = `${name}-${assetId}`

      const savedFilename = await downloadFile(url, filename)
      savedFiles.push(savedFilename)
      success++
    } catch (error) {
      console.error(`✗ Failed: ${name} - ${error.message}`)
      failed++
    }

    // 添加小延迟避免请求过快
    await new Promise(r => setTimeout(r, 100))
  }

  console.log(`\n=================================`)
  console.log(`Download complete!`)
  console.log(`Success: ${success}`)
  console.log(`Failed: ${failed}`)
  console.log(`=================================`)

  // 输出文件类型统计
  const svgFiles = savedFiles.filter(f => f.endsWith('.svg'))
  const pngFiles = savedFiles.filter(f => f.endsWith('.png'))
  const jpgFiles = savedFiles.filter(f => f.endsWith('.jpg'))

  console.log(`\nFile types:`)
  console.log(`  SVG: ${svgFiles.length}`)
  console.log(`  PNG: ${pngFiles.length}`)
  console.log(`  JPG: ${jpgFiles.length}`)
}

main()
