const dapp = "WaxelWorld",
    // ### Testnet
    chainId = "f16b1833c747c43682f4386fca9cbb327929334a762755ebec17f6f23c9b8a12",
    nodeEndpointArr = ["testnet.wax.pink.gg", "waxtestnet.greymass.com"],
    apiEndpointArr = ["test.wax.api.atomicassets.io", "atomic-wax-testnet.wecan.dev"],
    contract = "waxelsctest5",
    //-------------------------
    // ### Mainnet
    // chainId = "1064487b3cd1a897ce03ae5b6a865651747e2e152090f99c1d19d44e01aea5a4",
    // nodeEndpointArr = ["wax.pink.gg", "wax.greymass.com", "wax-public1.neftyblocks.com", "wax.cryptolions.io", "api.waxsweden.org", "wax.api.eosnation.io", "wax.eoseoul.io"],
    // apiEndpointArr = ["aa-wax-public1.neftyblocks.com", "wax-aa.eu.eosamsterdam.net", "atomic3.hivebp.io", "atomic.wax.eosrio.io", "wax.api.atomicassets.io"],
    // contract = "waxelworldsc",
    // ----------------
    collectionName = "waxelninjas1",
    ninjaSchema = "waxel.ninjas",
    ninja_arr = ["Human", "Orc", "Undead", "Elf", "Demon"],
    professions_arr = ["Miner", "Farmer", "Lumberjack", "Blacksmith", "Carpenter", "Tailor", "Engineer"],
    work_status = ["Mining", "Crafting", "Refining"];

let nodeEndpoint = nodeEndpointArr[0];
let apiEndpoint = apiEndpointArr[0];

let wax = new waxjs.WaxJS({
    rpcEndpoint: "https://" + nodeEndpoint,
    tryAutoLogin: !1
});

var loggedIn = !1,
    anchorAuth = "owner";
let configData = "",
    userAccount = "",
    total_matCount = "",
    refined_mat = "",
    function_call_count = 0;
let citizens_pack = [];
let userData = "";
async function autoLogin(node_idx, api_idx) {
    await wallet_isAutoLoginAvailable(node_idx, api_idx) && login(node_idx, api_idx)
}
async function wallet_isAutoLoginAvailable(node_idx, api_idx) {
    nodeEndpoint = nodeEndpointArr[node_idx];
    apiEndpoint = apiEndpointArr[api_idx];
    wax = new waxjs.WaxJS({
        rpcEndpoint: "https://" + nodeEndpoint,
        tryAutoLogin: !1
    });
    const t = new AnchorLinkBrowserTransport,
        a = new AnchorLink({
            transport: t,
            chains: [{
                chainId: chainId,
                nodeUrl: "https://" + nodeEndpoint
            }]
        });
    var e = await a.listSessions(dapp);
    return e && e.length > 0 ? (useAnchor = !0, !0) : (useAnchor = !1, await wax.isAutoLoginAvailable())
}
async function selectWallet(t, i, j) {
    wallet_selectWallet(t), login(i, j)
}
async function wallet_selectWallet(t) {
    useAnchor = "anchor" == t
}
async function login(node_idx, api_idx) {
    try {
        userAccount = await wallet_login(node_idx, api_idx), sendUserData()
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function wallet_login(node_idx = 0, api_idx = 0) {
    nodeEndpoint = nodeEndpointArr[node_idx];
    apiEndpoint = apiEndpointArr[api_idx];
    wax = new waxjs.WaxJS({
        rpcEndpoint: "https://" + nodeEndpoint,
        tryAutoLogin: !1
    });
    const t = new AnchorLinkBrowserTransport,
        a = new AnchorLink({
            transport: t,
            chains: [{
                chainId: chainId,
                nodeUrl: "https://" + nodeEndpoint
            }]
        });
    if (useAnchor) {
        var e = await a.listSessions(dapp);
        e && e.length > 0 ? wallet_session = await a.restoreSession(dapp) : wallet_session = (await a.login(dapp)).session, wallet_userAccount = String(wallet_session.auth).split("@")[0], auth = String(wallet_session.auth).split("@")[1], anchorAuth = auth
    } else wallet_userAccount = await wax.login(), wallet_session = wax.api, anchorAuth = "active";
    return wallet_userAccount
}

/*****************************************************/
/*****************  Mini Game login ******************/
/*****************************************************/

async function miniLogin() {
    sendUserData();
}

/*****************************************************/
/******************** Mini Game end ******************/
/*****************************************************/

async function fetchingData() {
    unityInstance.SendMessage("GameController", "Client_SetFetchingData", "true")
}
async function reload() {
    try {
        userData = await getUserData(), inventory = await getUserInventoryData(userData.mat_inventory), nft_count = await getNftCount(userData.nft_counts), ninjaData = await getNinjaData(), settlementData = await getSettlementData(), professionData = await getProfessionData(), configData = await getConfigData(), itemData = await getItemsData(configData[0]), assetData = await getAssets("all"), await getCitizensPack(assetData), extraData = await getExtraData(), dropData = await getDrop(), delayData = await getDelayData();
        let t = {
            account: userAccount.toString(),
            ninjas: ninjaData,
            professions: professionData,
            items: itemData,
            citizens: void 0 !== userData.citizen_count ? userData.citizen_count : 0,
            citizens_pack_count: citizens_pack.length,
            inventory: inventory,
            assets: assetData,
            nft_count: nft_count,
            extraData: extraData,
            config: configData[0],
            drop: dropData,
            settlements: settlementData,
            total_matCount: total_matCount,
            craft_combos: configData[1],
            finding_delay: delayData[0],
            refining_delay: delayData[1]
        };
        unityInstance.SendMessage("GameController", "Client_ReloadSyncDdata", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
        getAssetD();
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function sendUserData() {
    try {
        unityInstance.SendMessage("GameController", "Client_SetFetchingData", "true"), userData = await getUserData(), inventory = await getUserInventoryData(userData.mat_inventory), nft_count = await getNftCount(userData.nft_counts), ninjaData = await getNinjaData(), settlementData = await getSettlementData(), professionData = await getProfessionData(), configData = await getConfigData(), itemData = await getItemsData(configData[0]), assetData = await getAssets("all"), await getCitizensPack(assetData), extraData = await getExtraData(),  dropData = await getDrop(), delayData = await getDelayData();
        let t = {
            account: userAccount.toString(),
            ninjas: ninjaData,
            professions: professionData,
            items: itemData,
            citizens: void 0 !== userData.citizen_count ? userData.citizen_count : 0,
            citizens_pack_count: citizens_pack.length,
            inventory: inventory,
            assets: assetData,
            nft_count: nft_count,
            extraData: extraData,
            config: configData[0],
            drop: dropData,
            settlements: settlementData,
            total_matCount: total_matCount,
            craft_combos: configData[1],
            finding_delay: delayData[0],
            refining_delay: delayData[1]
        };
        unityInstance.SendMessage("GameController", "Client_SetLoginData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
        getAssetD()
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function getAssets(t) {
    try {
        if ("all" == t) {
            var a = "/atomicassets/v1/assets?collection_name=" + collectionName + "&owner=" + userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
            const t = await fetch("https://" + apiEndpoint + a, {
                    headers: {
                        "Content-Type": "text/plain"
                    },
                    method: "POST"
                }),
                n = await t.json(),
                s = Object.values(n.data);
            var e = [];
            for (const t of s) e.push({
                asset_id: t.asset_id,
                img: t.template.immutable_data.img,
                name: t.template.immutable_data.name,
                schema: t.schema.schema_name,
                template: t.template.template_id
            });
            return e
        } {
            a = "/atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=" + t + "&owner=" + userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
            const e = await fetch("https://" + apiEndpoint + a, {
                headers: {
                    "Content-Type": "text/plain"
                },
                method: "POST"
            });
            return (await e.json()).data
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const getDrop = async function () {
    var t = JSON.stringify({
        json: !0,
        code: "neftyblocksd",
        scope: "neftyblocksd",
        table: "drops",
        limit: 1,
        lower_bound: "29449",
        upper_bound: "29449"
    });
    const a = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
            headers: {
                "Content-Type": "text/plain"
            },
            body: t,
            method: "POST"
        }),
        e = await a.json();
    let n = "";
    return 0 != e.rows.length && (n = parseInt(e.rows[0].current_claimed) >= parseInt(e.rows[0].max_claimable) ? "https://wax.atomichub.io/market?collection_name=" + collectionName + "&order=asc&schema_name=" + ninjaSchema + "&sort=price&symbol=WAX" : "https://neftyblocks.com/c/"+ collectionName +"/drops/29449"), n
};
async function getUserData() {
    var t = JSON.stringify({
        json: !0,
        code: contract,
        scope: contract,
        table: "user",
        limit: 1,
        lower_bound: userAccount,
        upper_bound: userAccount
    });
    const a = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
            headers: {
                "Content-Type": "text/plain"
            },
            body: t,
            method: "POST"
        }),
        e = await a.json();
    return 0 != e.rows.length ? e.rows[0] : 0
}
async function getUserInventoryData(t) {
    inv_obj = [];
    let a = 0;
    if (t)
        for (i = 0; i < t.length; i++) inv = t[i].split(" "), a += parseFloat(inv[0]), inv_obj.push({
            count: (parseInt(inv[0] * 100) / 100).toString(),
            name: inv[1]
        });
    return total_matCount = a, inv_obj
}
const getNftCount = async t => {
    if (nft_count = [], t) {
        nft_count.push({
            count: t.maxNinja,
            name: "Max Ninja"
        });
        const a = Object.values(t.maxProfessions);
        for (const t of a) nft_count.push({
            count: t.value,
            name: t.key
        })
    }
    return nft_count
};
async function getAssetD() {
    try {
        let t = getAssets("all");
        assetdata = await t, unityInstance.SendMessage("GameController", "Client_SetAssetData", void 0 === assetdata ? JSON.stringify({}) : JSON.stringify(assetdata))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function getNinjaData() {
    try {
        let a = getAssets(ninjaSchema);
        assetData = await a;
        var t = [];
        const e = Object.values(assetData);
        if (0 != e.length) {
            const a = await checkAssetIds("ninjas");
            if (void 0 !== a) {
                const n = a[0];
                for (const s of e)
                    if (n.includes(s.asset_id)) {
                        const e = a[1];
                        for (const a of e) a.id == s.asset_id && t.push({
                            asset_id: s.asset_id,
                            mint_id: s.template_mint,
                            delay_seconds: a.delay_seconds,
                            last_search: a.last_search,
                            race: s.data.Race,
                            img: s.data.img,
                            status: a.status,
                            reg: "1"
                        })
                    } else t.push({
                        asset_id: s.asset_id,
                        mint_id: s.template_mint,
                        delay_seconds: "",
                        last_search: "",
                        race: s.data.Race,
                        img: s.data.img,
                        status: "",
                        reg: "0"
                    })
            } else
                for (const a of e) t.push({
                    asset_id: a.asset_id,
                    mint_id: a.template_mint,
                    delay_seconds: "",
                    last_search: "",
                    race: a.data.Race,
                    img: a.data.img,
                    status: "",
                    reg: "0"
                })
        }
        return t
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const getSettlementData = async () => {
    try {
        const t = await getAssets("upgrades");
        const a = Object.values(t),
            e = [],
            n = await checkAssetIds("settlements"),
            s = n[0],
            i = n[1];
        if (void 0 !== s && void 0 !== s)
            for (const t of i) e.push({
                asset_id: t.id,
                name: t.name,
                reg: "1"
            });
        if (0 !== a.length)
            for (const t of a) e.push({
                asset_id: t.asset_id,
                name: t.name,
                img: t.data.img,
                reg: "0"
            });
        return e
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, getConfigData = async () => {
    try {
        var t = JSON.stringify({
            json: !0,
            code: contract,
            scope: contract,
            table: "configs",
            limit: 1
        });
        const a = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
                headers: {
                    "Content-Type": "text/plain"
                },
                body: t,
                method: "POST"
            }),
            e = await a.json(),
            n = Object.values(await GetTemplateData());
        let s = [];
        if (0 === e.rows.length) return ""; {
            const t = Object.values(e.rows[0].item_combo);
            for (const a of t) {
                let t = [];
                for (const e of Object.values(a.ingredients)) {
                    let a = e.split(" ");
                    t.push({
                        key: a[1],
                        value: parseInt(a[0]).toString()
                    })
                }
                for (const e of n)
                    if (e.template_id == a.template_id) {
                        let n = e.name.split(" "),
                            i = [];
                        for (let t = 1; t < n.length; t++) i.push(n[t]);
                        s.push({
                            item_name: i.join(" "),
                            rarity: e.rarity,
                            template_id: a.template_id,
                            type: a.type,
                            delay: a.delay,
                            ingredients: t
                        });
                        break
                    }
            }
            return [e.rows[0], s]
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, getExtraData = async () => {
    try {
        var t = JSON.stringify({
            json: !0,
            code: contract,
            scope: contract,
            table: "extras",
        });
        const a = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
                headers: {
                    "Content-Type": "text/plain"
                },
                body: t,
                method: "POST"
            }),
            e = await a.json();
        n = Object.values(e.rows)
        s = [];
        for(const t of n)
        {
            s.push({
                searcher_name: t.searcher_name,
                schema_name: t.schema_name,
                template_id: t.template_id.toString(),
            })
        }
        return s;
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, getDelayData = async () => {
    try {
        var t = JSON.stringify({
            json: !0,
            code: contract,
            scope: contract,
            table: "delays",
            limit: 1
        });
        const a = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
                headers: {
                    "Content-Type": "text/plain"
                },
                body: t,
                method: "POST"
            }),
            e = await a.json();
        if (0 === e.rows.length) return ""; {
            const t = e.rows[0].finding_delay;
            const s = e.rows[0].refining_delay;
            return [t, s]
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, GetTemplateData = async () => {
    const t = await fetch("https://"+ apiEndpoint +"/atomicassets/v1/templates?collection_name=" + collectionName + "&schema_name=items&page=1&limit=1000&order=desc&sort=created", {
            headers: {
                "Content-Type": "text/plain"
            },
            method: "POST"
        }),
        a = await t.json(),
        e = [];
    for (const t of a.data) e.push({
        template_id: t.template_id,
        name: t.immutable_data.name,
        rarity: t.immutable_data.Rarity
    });
    return e
}, checkAssetIds = async t => {
    try {
        var a = JSON.stringify({
            json: !0,
            code: contract,
            scope: contract,
            table: t,
            key_type: "i64",
            index_position: 2,
            lower_bound: eosjsName.nameToUint64(wallet_userAccount),
            limit: 1e3
        });
        const e = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
                headers: {
                    "Content-Type": "text/plain"
                },
                body: a,
                method: "POST"
            }),
            n = await e.json(),
            s = Object.values(n.rows),
            i = [],
            r = [];
        if (0 != s.length)
            for (const a of s) i.push(a.asset_id), "ninjas" == t ? r.push({
                id: a.asset_id,
                delay_seconds: a.delay_seconds,
                last_search: a.last_search,
                status: a.status
            }) : "professions" == t ? r.push({
                id: a.asset_id,
                type: a.type,
                last_material_search: a.last_material_search,
                uses_left: a.uses_left,
                items: a.items,
                status: a.status
            }) : "items" == t ? r.push({
                id: a.asset_id,
                profession: a.profession,
                function_name: a.function.key,
                function_value: a.function.value,
                equipped: a.equipped,
                last_material_search: a.last_material_search,
                uses_left: a.uses_left,
                status: a.status
            }) : "extras" == t ? r.push({
                searcher_name: a.searcher_name,
                schema_name: a.schema_name,
                template_id: a.template_id,
            }) : "settlements" == t && userAccount == a.owner && r.push({
                id: a.asset_id,
                name: a.type
            });
        return [i, r]
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
};
async function getProfessionData() {
    try {
        let a = await getAssets("professions");
        var t = [];
        const e = Object.values(a);
        if (0 !== e.length) {
            const a = await checkAssetIds("professions");
            if (void 0 !== a) {
                const n = a[0];
                for (const s of e)
                    if (n.includes(s.asset_id)) {
                        const e = a[1];
                        for (const a of e) a.id == s.asset_id && t.push({
                            asset_id: s.asset_id,
                            type: a.type,
                            name: s.name,
                            last_material_search: a.last_material_search,
                            uses_left: a.uses_left,
                            items: a.items,
                            status: a.status,
                            img: s.data.img,
                            reg: "1"
                        })
                    } else {
                        if(s.data["Work (uses left)"]) {
                            t.push({
                                asset_id: s.asset_id,
                                type: "",
                                name: s.name,
                                last_material_search: "",
                                uses_left: s.data["Work (uses left)"],
                                items: "",
                                status: "",
                                img: s.data.img,
                                reg: "0"
                            })
                        }
                        else {
                            let uses;
                            if (s.name == "Miner" || s.name == "Farmer" || s.name == "Lumberjack") uses = "20";
                            else if (s.name != "Engineer") {
                                uses = "60";
                            } else {
                                uses = "30";
                            }

                            t.push({
                                asset_id: s.asset_id,
                                type: "",
                                name: s.name,
                                last_material_search: "",
                                uses_left: uses,
                                items: "",
                                status: "",
                                img: s.data.img,
                                reg: "0"
                            })
                        }
                    }
            } else {

                let uses;
                if (s.name == "Miner" || s.name == "Farmer" || s.name == "Lumberjack") uses = "20";
                else if (s.name != "Engineer") {
                    uses = "60";
                } else {
                    uses = "30";
                }

                for (const a of e) t.push({
                    asset_id: a.asset_id,
                    type: "",
                    name: a.name,
                    last_material_search: "",
                    uses_left: uses,
                    items: "",
                    status: "",
                    img: a.data.img,
                    reg: "0"
                })
            }
        }
        return t
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function getItemsData(t) {
    try {
        let a = await getAssets("items");
        const e = [],
            n = Object.values(a);
        if (0 !== n.length) {
            const a = await checkAssetIds("items");
            if (void 0 !== a) {
                const s = a[0];
                for (const i of n)
                    if (s.includes(i.asset_id)) {
                        const t = a[1];
                        for (const a of t) a.id == i.asset_id && e.push({
                            asset_id: i.asset_id,
                            name: i.name,
                            profession: a.profession,
                            function_name: a.function_name,
                            function_value: parseFloat(a.function_value).toFixed(2).toString(),
                            equipped: a.equipped,
                            last_material_search: a.last_material_search,
                            uses_left: a.uses_left,
                            status: a.status,
                            img: i.data.img
                        })
                    } else {
                        let a, n = i.template.immutable_data.Rarity;
                        a = "Rare" == n ? "50.00" : "Uncommon" == n ? "25.00" : "10.00";
                        const s = Object.values(t.item_combo);
                        for (const t of s)
                            if (i.template.template_id == t.template_id) {
                                if(i.mutable_data["Durability (uses left)"]) {
                                    e.push({
                                        asset_id: i.asset_id,
                                        name: i.name,
                                        profession: "",
                                        function_name: t.type,
                                        function_value: a,
                                        equipped: "0",
                                        last_material_search: "",
                                        uses_left: i.mutable_data["Durability (uses left)"],
                                        status: "",
                                        img: i.data.img
                                    });
                                }
                                else {
                                    e.push({
                                        asset_id: i.asset_id,
                                        name: i.name,
                                        profession: "",
                                        function_name: t.type,
                                        function_value: a,
                                        equipped: "0",
                                        last_material_search: "",
                                        uses_left: "20",
                                        status: "",
                                        img: i.data.img
                                    });
                                }
                                break
                            }
                    }
            } else
                for (const t of n) e.push({
                    asset_id: t.asset_id,
                    name: t.name,
                    profession: "",
                    function_name: "",
                    function_value: "",
                    equipped: "",
                    last_material_search: "",
                    uses_left: "",
                    status: "",
                    img: t.data.img,
                    reg: "0"
                })
        }
        return e
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const getCitizensPack = async t => {
    try {
        citizens_pack = [];
        const a = Object.values(t);
        for (const t of a) "Citizens - 10x" == t.name && "citizens" == t.schema && citizens_pack.push(t.asset_id)
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
};
// const getMysteriousData = async t => {
//     try {
//         mysterious = [];
//         const a = Object.values(t);
//         for (const t of a) "extras" == t.schema && mysterious.push(t);
//         return mysterious;
//     } catch (err) {
//         unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
//     }
// };
async function getProfessionD() {
    try {
        let t = await getProfessionData();
        unityInstance.SendMessage("GameController", "Client_SetProfessionData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const getItemD = async () => {
    try {
        item_data_updated = await getItemsData(configData[0]), unityInstance.SendMessage("GameController", "Client_SetItemData", void 0 === item_data_updated ? JSON.stringify({}) : JSON.stringify(item_data_updated))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
};
async function getSearchD(t, a, e) {
    try {
        var n = JSON.stringify({
            json: !0,
            code: contract,
            scope: contract,
            table: t,
            limit: 1,
            lower_bound: a,
            upper_bound: a
        });
        const s = await fetch("https://" + nodeEndpoint + "/v1/chain/get_table_rows", {
                headers: {
                    "Content-Type": "text/plain"
                },
                body: n,
                method: "POST"
            }),
            i = await s.json(),
            r = [];
        if (0 != i.rows.length)
            if ("holdup" == i.rows[0].status || "holdup1" == i.rows[0].status)
                if (await delay(2e3), function_call_count++, function_call_count < 4) getSearchD(t, a, e);
                else {
                    let t = [];
                    t.push({
                        name: i.rows[0].name,
                        status: "RNG Failed !"
                    }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
                }
        else if ("holdup" != i.rows[0].status && "holdup1" != i.rows[0].status) {
            if (ninja_arr.includes(i.rows[0].race)) r.push({
                name: i.rows[0].race,
                status: i.rows[0].status,
                totalCitizensCount: (await getUserData()).citizen_count
            });
            else if (professions_arr.includes(i.rows[0].name))
                if ("Gatherer" == i.rows[0].type) switch (e) {
                    case "start_mat_find":
                        r.push({
                            name: i.rows[0].name,
                            status: i.rows[0].status,
                            asset_id: i.rows[0].asset_id,
                            matFound: "false",
                            matRefined: "false",
                            totalMatCount: total_matCount
                        });
                        break;
                    case "find_mat":
                        let t = [];
                        t = i.rows[0].status.split(" "), t.length < 3 ? mat_name=[""] : mat_name = t[3].match(/[a-zA-Z]+/g), t.length < 3 ? count = 0 : count = t[3].match(/\d+/g) / 100, r.push({
                            name: i.rows[0].name,
                            status: "",
                            matFound: "true",
                            matRefined: "false",
                            matName: mat_name[0],
                            matCount: count,
                            totalMatCount: total_matCount
                        });
                        break;
                    case "equip":
                        r.push({
                            name: i.rows[0].name,
                            status: "Item Equipped",
                            asset_id: i.rows[0].asset_id,
                            matFound: "false",
                            matRefined: "false",
                            equipped: "1",
                            totalMatCount: total_matCount,
                            items_ids: i.rows[0].items
                        });
                        break;
                    case "unequip":
                        r.push({
                            name: i.rows[0].name,
                            status: "De-Equiped",
                            asset_id: i.rows[0].asset_id,
                            matFound: "false",
                            matRefined: "false",
                            equipped: "0",
                            totalMatCount: total_matCount,
                            items_ids: i.rows[0].items
                        })
                } else if ("Refiner and crafter" == i.rows[0].type || "Refiner and Crafter" == i.rows[0].type) switch (e) {
                    case "refining":
                        let t, a = [];
                        a = i.rows[0].status.split("%");
                        for (const e of configData[0].rawmat_refined)
                            if (a[1] == e.value) {
                                t = e.key;
                                break
                            } r.push({
                            name: i.rows[0].name,
                            status: "refine",
                            matFound: "false",
                            matRefined: "true",
                            matName: t,
                            totalMatCount: (parseFloat(total_matCount)).toString()
                        });
                        break;
                    case "crafting":
                        let e, n = [];
                        n = i.rows[0].status.split("%");
                        await getItemD();
                        for (const t of configData[1])
                            if (n[1] == t.template_id) {
                                let a;
                                if ("Mining Cart" == t.item_name || "Wagon" == t.item_name || "Wheelbarrow" == t.item_name) switch (t.rarity) {
                                    case "Common":
                                        a = "Birch";
                                        break;
                                    case "Uncommon":
                                        a = "Oak";
                                        break;
                                    case "Rare":
                                        a = "Teak"
                                } else switch (t.rarity) {
                                    case "Common":
                                        a = "Copper";
                                        break;
                                    case "Uncommon":
                                        a = "Tin";
                                        break;
                                    case "Rare":
                                        a = "Iron"
                                }
                                e = a + " " + t.item_name;
                                break
                            } r.push({
                            name: i.rows[0].name,
                            status: "craft",
                            matFound: "false",
                            matRefined: "false",
                            matCrafted: "true",
                            matName: e,
                            totalMatCount: (parseFloat(total_matCount)).toString()
                        })
                }
            unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === r ? JSON.stringify({}) : JSON.stringify(r))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function start_search(t) {
    try {
        assetId = String(t).split("#")[1];
        await wallet_transact([{
            account: contract,
            name: "startsearch",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                assetID: assetId
            }
        }]);
        await getNinjaD(), await getNinjaSearchD(t)
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function search_citizen(t, a, e) {
    arr = t.split(",");
    try {
        const current_citizens = (await getUserData()).citizen_count;
        const old_ninja = await getNinjaData();
        "1" == a ? await wallet_transact([{
            account: contract,
            name: "startsearch",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                asset_ids: arr
            }
        }]) : await wallet_transact([{
            account: contract,
            name: "searchforcz",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                asset_ids: arr
            }
        }]);
        await delay(4e3), ninja_arr.includes(e) ? (await getNinjaD()) : professions_arr.includes(e) && (await getProfessionD())
        let s = [];
        if(a == "1") {
            if(ninja_arr.includes(e)){
                s.push({
                    name: e,
                    status: "Search Started"
                })
            }
            else
            {
                s.push({
                    name: e,
                    status: "Work Started"
                })
            }
            unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === s ? JSON.stringify({}) : JSON.stringify(s))
        }
        else {
            if(ninja_arr.includes(e))
            {
                let loops = 0;
                while(loops < 10)
                {
                    const new_citizens = (await getUserData()).citizen_count;
                    if(new_citizens != current_citizens) break;
                    loops++;
                    await delay(500);
                }
                await getAssetD();
            }
            ninja_arr.includes(e) ? (function_call_count = 0, await getSearchD("ninjas", arr[0], "")) : professions_arr.includes(e) && (function_call_count = 0, await getSearchD("professions", arr[0], "start_mat_find"))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const delay = async t => new Promise((a => {
    setTimeout((() => {
        a(2)
    }), t)
})), unregisterAsset = async function (t, a) {
    const arr = t.split(",");
    try {
        await wallet_transact([{
            account: contract,
            name: "unbindnfts",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                owner: wallet_userAccount,
                asset_ids: arr
            }
        }]);
        ninja_arr.includes(a) ? (await delay(2e3), await getNinjaD()) : professions_arr.includes(a) && (await delay(2e3), await getProfessionD());
        let e = [];
        e.push({
            name: a,
            status: "De-Registered Successfully"
        }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === e ? JSON.stringify({}) : JSON.stringify(e))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
};
async function wallet_transact(t) {
    if (useAnchor) a = {
        transaction_id: (a = await wallet_session.transact({
            actions: t
        }, {
            blocksBehind: 3,
            expireSeconds: 30
        })).processed.id
    };
    else var a = await wallet_session.transact({
        actions: t
    }, {
        blocksBehind: 3,
        expireSeconds: 120
    });
    return a
}
async function doLogoutAction() {
    localStorage.clear()
}
async function registerAsset(t, a) {
    const arr = t.split(",");
    try {
        await wallet_transact([{
            account: contract,
            name: "registernfts",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                asset_ids: arr,
                owner: wallet_userAccount
            }
        }]);
        ninja_arr.includes(a) ? (await delay(2e3), await getNinjaD()) : professions_arr.includes(a) && (await delay(2e3), await getProfessionD());
        let e = [];
        e.push({
            name: a,
            status: "Registered Successfully"
        }), unityInstance.SendMessage("GameController", "ResponseCallbackData", void 0 === e ? JSON.stringify({}) : JSON.stringify(e))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
async function getNinjaD() {
    try {
        let t = await getNinjaData();
        unityInstance.SendMessage("GameController", "Client_SetNinjaData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}
const mintcitizens = async () => {
    try {
        const t = await wallet_transact([{
            account: contract,
            name: "mintcitizens",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                amount: 1
            }
        }]);
        let a;
        await t, t.transaction_id && (a = "Mint");
        await delay(4000);
        // reload asset data and citizen pack for refreshing citizens_pack_count
        assetData = await getAssets("all"), await getCitizensPack(assetData);

        let e = {
            transactionid: a,
            citizens: (await getUserData()).citizen_count,
            citizens_pack_count: citizens_pack.length
        };
        unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === e ? JSON.stringify({}) : JSON.stringify(e))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, mintmat = async t => {
    try {
        const a = await wallet_transact([{
            account: contract,
            name: "mintmats",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                mat: t,
                amount: 1
            }
        }]);
        let e;
        await a, a.transaction_id && (e = "Mint " + t);
        await delay(4e3);
        await getAssetD();
        const n = await getUserData();
        inventory = await getUserInventoryData(n.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory));
        let s = {
            transactionid: e,
            citizens: total_matCount
        };
        unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === s ? JSON.stringify({}) : JSON.stringify(s))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, burncitizennft = async () => {
    try {
        let t = 0;
        if (citizens_pack.length > 0) {
            t = citizens_pack[0];
            const a = await wallet_transact([{
                account: "atomicassets",
                name: "burnasset",
                authorization: [{
                    actor: wallet_userAccount,
                    permission: anchorAuth
                }],
                data: {
                    asset_owner: wallet_userAccount,
                    asset_id: t
                }
            }]);
            let e;
            await a, a.transaction_id && (e = "Burn");
            citizens_pack.splice(0, 1)
            let n = {
                transactionid: e,
                citizens: (await getUserData()).citizen_count,
                citizens_pack_count: citizens_pack.length
            };
            unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === n ? JSON.stringify({}) : JSON.stringify(n))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, burn_itemsnft = async (t, a) => {
    try {
        const e = await getUserData(),
            n = await getUserInventoryData(e.mat_inventory),
            s = await wallet_transact([{
                account: "atomicassets",
                name: "burnasset",
                authorization: [{
                    actor: wallet_userAccount,
                    permission: anchorAuth
                }],
                data: {
                    asset_owner: wallet_userAccount,
                    asset_id: a,
                    memo: "burnrng"
                }
            }]);
        if (await s, void 0 !== s.transaction_id) {
            let a = [];
            let cnt = 0;
            await delay(3000);
            let find_flag = false;
            let s;
            while(cnt < 5)
            {
                const e = await getUserData();
                s = await getUserInventoryData(e.mat_inventory);
                if(n.length != s.length) {
                    for (const e of s)
                    {
                        let equal_flag = false;
                        for (const t of n)
                        {
                            if(t.name == e.name) {
                                equal_flag = true;
                                break;
                            }
                        }
                        if(equal_flag == false) {
                            a.push({
                                name: e.name,
                                count: parseFloat(e.count).toString()
                            });
                            find_flag = true;
                            break;
                        }
                    }
                }
                else {
                    for (const t of n)
                    {
                        for (const e of s)
                            if (t.name == e.name) {
                                if (t.count == e.count) break;
                                if (t.count != e.count) {
                                    a.push({
                                        name: e.name,
                                        count: (parseFloat(e.count) - parseFloat(t.count)).toString()
                                    });
                                    find_flag = true;
                                    break
                                }
                            }
                    }
                }
                if(find_flag == true) break;
                cnt++;
                await delay(500);
            }

            await getItemD(), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === s ? JSON.stringify({}) : JSON.stringify(s));
            let i = {
                transactionid: t,
                citizens: total_matCount
            };
            unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === i ? JSON.stringify({}) : JSON.stringify(i)), 0 == a.length ? (a.push({
                name: "null",
                count: 0
            }), unityInstance.SendMessage("GameController", "Client_SetBurnInventoryData", void 0 === a ? JSON.stringify({}) : JSON.stringify(a))) : unityInstance.SendMessage("GameController", "Client_SetBurnInventoryData", void 0 === a ? JSON.stringify({}) : JSON.stringify(a))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, burn_profession_nft = async (t, a) => {
    try {
        const arr = a.split(",");
        const e = (await getUserData()).citizen_count,
            n = await wallet_transact([{
                account: "atomicassets",
                name: "transfer",
                authorization: [{
                    actor: wallet_userAccount,
                    permission: anchorAuth
                }],
                data: {
                    from: wallet_userAccount,
                    to: contract,
                    asset_ids: arr,
                    memo: "burnprofess"
                }
            }]);
        if (await n, void 0 !== n.transaction_id) {
            // await delay(3000);
            // const a = (await getUserData()).citizen_count;
            // let n = a - e;
            let n = 0, cnt = 0;
            let a;
            while(cnt < 10 && n === 0) {
                a = (await getUserData()).citizen_count;
                n = a - e;
                delay(500);
                cnt++;
            }
            await getProfessionD(), await delay(300);
            let s = [];
            s.push({
                name: t,
                status: "Burnt Successfully"
            }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === s ? JSON.stringify({}) : JSON.stringify(s));
            let i = {
                transactionid: t + "%" + n,
                citizens: a
            };
            unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === i ? JSON.stringify({}) : JSON.stringify(i))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, GetAsset_TemplateData = async t => {
    var a = "/atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=materials&template_id=" + t + "&owner=" + userAccount + "&page=1&limit=1&order=desc&sort=asset_id";
    const e = await fetch("https://" + apiEndpoint + a, {
            headers: {
                "Content-Type": "text/plain"
            },
            method: "POST"
        }),
        n = await e.json();
    return 0 == n.data.length ? 0 : n.data[0].asset_id
}, burnmat = async t => {
    try {
        let temp_key;
        assetData = Object.values(await getAssets("materials"));
        for (const e of configData[0].template_ids)
            if (e.key == t) {
                temp_key = e.value;
                break
            } if (asset_id = await GetAsset_TemplateData(temp_key), 0 != asset_id) {
            const a = await wallet_transact([{
                account: "atomicassets",
                name: "burnasset",
                authorization: [{
                    actor: wallet_userAccount,
                    permission: anchorAuth
                }],
                data: {
                    asset_owner: wallet_userAccount,
                    asset_id: asset_id
                }
            }]);
            let e;
            await a, a.transaction_id && (e = "Burn " + t);
            let cnt = 0;
            while(true)
            {
                temp_id = await GetAsset_TemplateData(temp_key);
                if(temp_id != asset_id) break;
                cnt++;
                if(cnt > 3) break;
                delay(100);
            }
            await getAssetD();
            const n = await getUserData();
            inventory = await getUserInventoryData(n.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory));
            let s = {
                transactionid: e,
                citizens: total_matCount
            };
            unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === s ? JSON.stringify({}) : JSON.stringify(s))
        } else {
            let t = {
                transactionid: "None mat"
            };
            unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, SendUserMaxNftCount = async () => {
    try {
        userData = await getUserData(), max_count = await getNftCount(userData.nft_counts), unityInstance.SendMessage("GameController", "GetUserMaxNftCount", void 0 === max_count ? JSON.stringify({}) : JSON.stringify(max_count))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "SendUserMaxNftCount", err.message)
    }
}, example = async (t, a, e) => {}, transfer = async (t, a, e) => {
    try {
        await wallet_transact([{
            account: "atomicassets",
            name: "transfer",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                from: wallet_userAccount,
                to: contract,
                asset_ids: [t],
                memo: a
            }
        }]);
        if (await delay(2e3), "regupgrade" == a) {
            await SendUserMaxNftCount(), settlements_data = await getSettlementData(), unityInstance.SendMessage("GameController", "Client_SetSettlementData", void 0 === settlements_data ? JSON.stringify({}) : JSON.stringify(settlements_data));
            let t = [];
            t.push({
                // name: "Camp" == e ? "Camp" : "Forest",
                name: e,
                status: "Registered Successfully"
            }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, blendProfession = async (t, a, e) => {
    try {
        await wallet_transact([{
            account: "atomicassets",
            name: "transfer",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                from: wallet_userAccount,
                to: contract,
                asset_ids: [t],
                memo: a
            }
        }]);
        if (await delay(2e3), "blendprofession" == a) {
            await SendUserMaxNftCount(), await getProfessionD();
            await getAssetD();
            let t = [];
            t.push({
                // name: "Camp" == e ? "Camp" : "Forest",
                name: e,
                status: "Blended Successfully",
                totalCitizensCount: void 0 !== userData.citizen_count ? userData.citizen_count : 0
            }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === t ? JSON.stringify({}) : JSON.stringify(t))
        }
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, equipItems = async (t, a) => {
    try {
        await wallet_transact([{
            account: contract,
            name: "equipitems",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                item_ids: [a],
                profession_id: t
            }
        }]);
        await delay(3e3), await getProfessionD(), function_call_count = 0, await getItemD(), await getSearchD("professions", t, "equip")
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, unequipItems = async (t, a, e) => {
    try {
        const arr = t.split(",");
        await wallet_transact([{
            account: contract,
            name: "unequipitems",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                professID: e,
                item_ids: arr
            }
        }]);
        await getProfessionD(), function_call_count = 0, await getItemD(), await getSearchD("professions", e, "unequip");
        let n = {
            transactionid: a,
            citizens: ""
        };
        unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === n ? JSON.stringify({}) : JSON.stringify(n))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, withdraw_asset = async (t, a) => {
    try {
        await wallet_transact([{
            account: contract,
            name: "withdrawnfts",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                asset_ids: [t],
                account: wallet_userAccount
            }
        }]);
        await delay(2e3), await SendUserMaxNftCount(), settlements_data = await getSettlementData(), unityInstance.SendMessage("GameController", "Client_SetSettlementData", void 0 === settlements_data ? JSON.stringify({}) : JSON.stringify(settlements_data));
        let e = [];
        e.push({
            // name: "Camp" == a ? "Camp" : "Forest",
            name: a,
            status: "De-Registered Successfully"
        }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === e ? JSON.stringify({}) : JSON.stringify(e))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, find_mat = async t => {
    try {
        const a = await getUserData();
        n = await getUserInventoryData(a.mat_inventory);
        const arr = t.split(",");
        await wallet_transact([{
            account: contract,
            name: "findmat",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                asset_ids: arr,
                account: wallet_userAccount
            }
        }]);
        await delay(3600), await getProfessionD();
        let loops = 0;
        let inventory;
        while(loops < 10)
        {
            const a = await getUserData();
            s = await getUserInventoryData(a.mat_inventory);
            if(n.length != s.length) 
                break;
            else 
            {
                let find_flag = false;
                for (const t of n)
                {
                    if(find_flag == true) break;
                    for (const e of s)
                        if (t.name == e.name) {
                            if (t.count == e.count) break;
                            if (t.count != e.count) {
                                inventory = s;
                                find_flag = true;
                                break;
                            }
                        }
                }
                if(find_flag == true) break;
            }
            loops++;
            await delay(500);
        }
        await getAssetD();
        await getItemD();
        unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory)), function_call_count = 0, await getSearchD("professions", arr[0], "find_mat")
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, mat_refine = async (t, a, e) => {
    try {
        await wallet_transact([{
            account: contract,
            name: "startcraft",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount,
                assetID: t,
                mat: a,
                refining: true
            }
        }]);
        await delay(2e3), await getProfessionD();
        const n = await getUserData();
        inventory = await getUserInventoryData(n.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory));
        let s = [];
        s.push({
            name: e,
            status: "Refining Started"
        }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === s ? JSON.stringify({}) : JSON.stringify(s))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, mat_craft = async (t, a, e) => {
    try {
        await wallet_transact([{
            account: contract,
            name: "startcraft",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                assetID: t,
                account: wallet_userAccount,
                mat: a,
                refining: false
            }
        }]);
        await delay(2e3), await getProfessionD();
        const n = await getUserData();
        inventory = await getUserInventoryData(n.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory));
        let s = [];
        s.push({
            name: e,
            status: "Crafting Started"
        }), unityInstance.SendMessage("GameController", "Client_SetCallBackData", void 0 === s ? JSON.stringify({}) : JSON.stringify(s))
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, mat_refine_comp = async t => {
    try {
        await wallet_transact([{
            account: contract,
            name: "refinemat",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                professID: t,
                account: wallet_userAccount
            }
        }]);
        await delay(3600), await getProfessionD(), function_call_count = 0;
        const a = await getUserData();
        inventory = await getUserInventoryData(a.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory)), await getSearchD("professions", t, "refining")
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, mat_craft_comp = async t => {
    try {
        await wallet_transact([{
            account: contract,
            name: "craftitem",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                professID: t,
                account: wallet_userAccount
            }
        }]);
        await delay(3600), await getProfessionD(), function_call_count = 0;
        const a = await getUserData();
        inventory = await getUserInventoryData(a.mat_inventory), unityInstance.SendMessage("GameController", "Client_SetInventoryData", void 0 === inventory ? JSON.stringify({}) : JSON.stringify(inventory)), await getSearchD("professions", t, "crafting")
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
}, minigame_start = async() => {
    try {
        await wallet_transact([{
            account: contract,
            name: "minilogin",
            authorization: [{
                actor: wallet_userAccount,
                permission: anchorAuth
            }],
            data: {
                account: wallet_userAccount
            }
        }]);
        await delay(3600);
        let e = {
            citizens: (await getUserData()).citizen_count,
        };
        unityInstance.SendMessage("GameController", "Client_TrxHash", void 0 === e ? JSON.stringify({}) : JSON.stringify(e))
        // unityInstance.SendMessage("GameController", "Client_SetCallBackData", JSON.stringify({}));
    } catch (err) {
        unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", err.message)
    }
};
